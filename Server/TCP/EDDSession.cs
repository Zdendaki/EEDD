using Communication;
using Communication.Data;
using Communication.Procedures;
using Communication.Procedures.Basic;
using Communication.Procedures.Clients;
using Communication.Procedures.Users;
using Microsoft.EntityFrameworkCore;
using NetCoreServer;
using Newtonsoft.Json;
using ServerData;
using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using T = System.Timers;

namespace Server.TCP
{
    internal class EDDSession : TcpSession
    {
        List<TCPError> Errors { get; set; }
        
        readonly DiffieHellman diffie;
        readonly TCPServer server;
        ClientInfo client;
        DiffieHellman.AesKeys aesKeys;
        T.Timer timer;
        List<MessageData> sentMessages;
        object parseLock = new object();
        
        bool handshaked = false;

        public EDDSession(TcpServer server) : base (server)
        {
            this.server = (TCPServer)server;
            diffie = this.server.Diffie;
            Errors = new();
            timer = new(5000);
            sentMessages = new();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, T.ElapsedEventArgs e)
        {
            Ping p = new Ping();
            SendMessageAsync(p);

            if (sentMessages.Any(x => (DateTime.Now - x.Sent).TotalSeconds > 20d))
            {
                foreach (MessageData message in sentMessages.Where(x => (DateTime.Now - x.Sent).TotalSeconds > 20d).OrderBy(x => x.Sent))
                {
                    sentMessages.Remove(message);
                    SendMessageAsync(message.Message);
                    Thread.Sleep(50);
                }
            }
        }

        protected override void OnConnected()
        {
            Worker.Logger.LogInformation($"[{Id}] Client connected");
            client = new ClientInfo(Id);
            SendAsync(Utils.Combine(DiffieHellman.Handshake, diffie.PublicKey, DiffieHellman.Delimiter));
            Worker.Clients.Add(client);
            timer.Enabled = true;
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            lock (parseLock)
            {
                int j = 0;
                long i = offset;
                long segStart = offset;

                while (i < size)
                {
                    if (buffer[i] == DiffieHellman.Delimiter[j])
                    {
                        j++;
                    }
                    else
                    {
                        j = 0;
                    }
                    if (j == DiffieHellman.Delimiter.Length)
                    {
                        byte[] messageBuffer = new byte[i - segStart - DiffieHellman.Delimiter.LongLength + 1];
                        Array.Copy(buffer, segStart, messageBuffer, 0, messageBuffer.Length);
                        segStart = i + 1;
                        j = 0;
                        ProcessMessage(messageBuffer);
                    }
                    i++;
                }

                if (segStart < size)
                {
                    Worker.Logger.LogWarning($"Message from client [{Id}] was discarded");
                }
            }
        }

        private void ProcessMessage(byte[] buffer)
        {
            if (buffer.LongLength == 166 && buffer.StartsWith(DiffieHellman.Handshake))
            {
                Array.Copy(buffer, 8, client.PublicKey, 0, 158);
                aesKeys = diffie.GenerateKeys(client.PublicKey);
                handshaked = true;
                Worker.Logger.LogDebug($"[{Id}] Client handshaked");

                if (Worker.Clients.Any(x => x.PublicKey == client.PublicKey && x.GUID != Id)) // TODO: fix byte[] == byte[]
                {

                }

                return;
            }

            if (!handshaked)
            {
                Worker.Logger.LogWarning($"[{Id}] Client requested message without handshake");
                return;
            }

            string message = "";
            VoidProcedure? procedure;
            try
            {
                message = DecryptMessage(buffer);

                if (string.IsNullOrWhiteSpace(message))
                    return;

                procedure = JsonConvert.DeserializeObject<VoidProcedure>(message);
            }
            catch
            {
                Worker.Logger.LogWarning($"[{Id}] Couldn't parse message: " + message);
                return;
            }

            if (procedure is not null)
            {
                switch (procedure.Type)
                {
                    case ProcedureType.LoginRequest:
                        ProcessLogin(message);
                        break;
                    case ProcedureType.ClientsListRequest:
                        ProcessClientsListRequest(message);
                        break;
                    case ProcedureType.StartShiftRequest:
                        ProcessStartShiftRequest(message);
                        break;
                    case ProcedureType.ClientDataRequest:
                        ProcessClientDataRequest(message);
                        break;
                    case ProcedureType.Ping:
                        ProcessPing(message);
                        break;
                }

                if (message.Contains("RequestGUID"))
                {
                    VoidResponse? response = JsonConvert.DeserializeObject<VoidResponse>(message);
                    if (response is not null)
                        sentMessages.RemoveAll(x => x.Message.GUID == response?.RequestGUID);
                }
            }
        }

        protected override void OnDisconnected()
        {
            EndShift(); // TODO: možná změnit
            Worker.Logger.LogInformation($"[{Id}] Client disconnected");
            timer.Enabled = false;
        }

        protected override void OnError(SocketError error)
        {
            Errors.Add(new(error));
        }

        internal bool SendMessageAsync(Procedure message)
        {
            if (handshaked)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                bool sent = SendAsync(diffie.Encrypt(buffer, aesKeys));
                if (message is not IResponse)
                    sentMessages.Add(new(message));
                return sent;
            }
            else
                return false;
        }

        internal bool MulticastMessage(object? message)
        {
            if (handshaked)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                return Server.Multicast(diffie.Encrypt(buffer, aesKeys));
            }
            else
                return false;
        }

        internal string DecryptMessage(byte[] data)
        {
            return Encoding.UTF8.GetString(diffie.Decrypt(data, aesKeys));
        }

        #region Process requests
        private void ProcessLogin(string data)
        {
            Worker.Logger.LogInformation($"[{Id}] Login requested");
            LoginRequest? request = JsonConvert.DeserializeObject<LoginRequest>(data);
            if (request is not null)
            {
                var (res, u) = ServerWorker.DoLogin(request, UserRole.Dispatcher);
                client.User = u;
                if (SendMessageAsync(res))
                    Worker.Logger.LogInformation($"[{Id}] Login response sent");
            }
        }

        private void ProcessClientsListRequest(string data)
        {
            ClientsListRequest? request = JsonConvert.DeserializeObject<ClientsListRequest>(data);

            if (request is not null)
            {
                SendMessageAsync(ServerWorker.GetClients(request));
            }
        }

        private void ProcessStartShiftRequest(string data)
        {
            using (Context database = new Context())
            {
                StartShiftRequest? request = JsonConvert.DeserializeObject<StartShiftRequest>(data);

                if (request is not null)
                {
                    (TokenState token, User? user) = database.CheckUser(request.Token, UserRole.Dispatcher);

                    if (token == TokenState.Expired)
                        SendMessageAsync(new StartShiftResponse(request.GUID, ResponseState.ExpiredToken, false));
                    else if (token == TokenState.UnsufficentRights)
                        SendMessageAsync(new StartShiftResponse(request.GUID, ResponseState.UnsufficentRights, false));
                    else if (token == TokenState.Ok)
                    {
                        if (user is null)
                        {
                            SendMessageAsync(new StartShiftResponse(request.GUID, ResponseState.InvalidToken, false));
                            return;
                        }

                        Client? client = database.Routes
                            .Include(x => x.Users).Include(x => x.Clients).ThenInclude(x => x.Stations)
                            .FirstOrDefault(x => x.Users.Contains(user) && x.Clients.Any(x => x.Id == request.ClientId))?.Clients.SingleOrDefault(x => x.Id == request.ClientId);

                        if (client is null)
                        {
                            SendMessageAsync(new StartShiftResponse(request.GUID, ResponseState.UnsufficentRights, false));
                            return;
                        }

                        bool occupied = client.Shifts.Any(x => x.StartTime is not null && x.EndTime is null);

                        if (!occupied)
                        {
                            Shift shift = new Shift()
                            {
                                StartTime = DateTime.Now,
                                User = user,
                                Client = client
                            };
                            var sh = database.Shifts.Add(shift);
                            database.SaveChanges();
                            SendMessageAsync(new StartShiftResponse(request.GUID, ResponseState.Success, true, sh.Entity.Id));
                        }
                        else
                        {
                            SendMessageAsync(new StartShiftResponse(request.GUID, ResponseState.Success, false));
                        }
                    }
                    else
                        SendMessageAsync(new StartShiftResponse(request.GUID, ResponseState.InvalidToken, false));
                }
            }
        }

        private void ProcessClientDataRequest(string data)
        {
            ClientDataRequest? request = JsonConvert.DeserializeObject<ClientDataRequest>(data);

            if (request is not null)
            {
                SendMessageAsync(ServerWorker.GetClientData(request));
            }
        }

        private void ProcessPing(string data)
        {
            Ping? request = JsonConvert.DeserializeObject<Ping>(data);
            if (request is not null)
            {
                SendMessageAsync(new Pong(request.GUID));
            }
        }

        #endregion

        private void EndShift()
        {
            if (client.Shift is not null)
            {
                using (Context context = new Context())
                {
                    Shift shift = context.Shifts.First(x => x.Id == client.Shift.Id);
                    shift.EndTime = DateTime.Now;
                    context.SaveChanges();
                    client.Shift = null;
                }
            }
        }
    }
}
