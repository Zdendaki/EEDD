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
    internal class EDDSession : WsSession
    {        
        List<TCPError> Errors { get; set; }
        
        readonly DiffieHellman diffie;
        readonly WSServer server;
        ClientInfo client;
        DiffieHellman.AesKeys aesKeys;
        T.Timer timer;
        List<MessageData> sentMessages;
        
        bool handshaked = false;

        public EDDSession(WsServer server) : base (server)
        {
            this.server = (WSServer)server;
            diffie = this.server.Diffie;
            Errors = new();
            timer = new(5000);
            sentMessages = new();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, T.ElapsedEventArgs e)
        {
            SendPingAsync("");

            if (sentMessages.Any(x => (DateTime.Now - x.Sent).TotalSeconds > 20d))
            {
                foreach (MessageData message in sentMessages.Where(x => (DateTime.Now - x.Sent).TotalSeconds > 20d).OrderBy(x => x.Sent))
                {
                    sentMessages.Remove(message);
                    SendMessage(message.Message);
                    Thread.Sleep(50);
                }
            }
        }

        public override void OnWsConnected(HttpRequest request)
        {
            Worker.Logger.LogInformation($"[{Id}] Client connected");
            client = new ClientInfo(Id);
            if (server.Encryption)
                SendBinaryAsync(Utils.Combine(DiffieHellman.Handshake, diffie.PublicKey));
            Worker.Clients.Add(client);
            timer.Enabled = true;
        }

        public override void OnWsPong(byte[] buffer, long offset, long size)
        {
            base.OnWsPong(buffer, offset, size);
        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            byte[] message = buffer.Cut(offset, size);

            if (server.Encryption && !handshaked && message.LongLength == 166L && message.StartsWith(DiffieHellman.Handshake))
            {
                Array.Copy(message, 8, client.PublicKey, 0, 158);
                aesKeys = diffie.GenerateKeys(client.PublicKey);
                handshaked = true;
                Worker.Logger.LogDebug($"[{Id}] Client handshaked");

                if (Worker.Clients.Any(x => Utils.Compare(x.PublicKey, client.PublicKey) && x.GUID != Id))
                {

                }

                return;
            }

            if (handshaked || !server.Encryption)
                ProcessMessage(message);
            else
                Worker.Logger.LogWarning($"[{Id}] Client requested message without handshake");
        }

        private void ProcessMessage(byte[] buffer)
        {
            string message = "";
            VoidProcedure? procedure;
            try
            {
                message = DecryptMessage(buffer).Trim();

                if (string.IsNullOrWhiteSpace(message))
                    return;

                procedure = JsonConvert.DeserializeObject<VoidProcedure>(message);
            }
            catch
            {
                Worker.Logger.LogWarning($"[{Id}] Couldn't parse message: " + Encoding.UTF8.GetString(buffer));
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
                        sentMessages.RemoveAll(x => Utils.Compare(x.Message.GUID, response?.RequestGUID));
                }
            }
        }

        public override void OnWsDisconnected()
        {
#if DEBUG
            EndShift(); // TODO: možná změnit
#else
            
#endif
            Worker.Logger.LogInformation($"[{Id}] Client disconnected");
            Worker.Clients.Remove(client);
            timer.Enabled = false;
        }

        protected override void OnError(SocketError error)
        {
            Errors.Add(new(error));
        }

        internal bool SendMessage(Procedure message)
        {
            if (handshaked || !server.Encryption)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                byte[] output = diffie.Encrypt(buffer, aesKeys);
                bool sent = SendBinaryAsync(output);
                if (message is not IResponse && message is not Ping)
                    sentMessages.Add(new(message));
                return sent;
            }
            else
                return false;
        }

        internal bool MulticastMessage(object? message)
        {
            if (handshaked || !server.Encryption)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                byte[] output = diffie.Encrypt(buffer, aesKeys);
                return Server.Multicast(output);
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
                if (SendMessage(res))
                    Worker.Logger.LogInformation($"[{Id}] Login response sent");
            }
        }

        private void ProcessClientsListRequest(string data)
        {
            ClientsListRequest? request = JsonConvert.DeserializeObject<ClientsListRequest>(data);

            if (request is not null)
            {
                SendMessage(ServerWorker.GetClients(request));
            }
        }

        private void ProcessStartShiftRequest(string data)
        {
            Worker.Logger.LogInformation($"[{Id}] Start shift requested");
            using (Context database = new Context())
            {
                StartShiftRequest? request = JsonConvert.DeserializeObject<StartShiftRequest>(data);

                if (request is not null)
                {
                    (TokenState token, User? user) = database.CheckUser(request.Token, UserRole.Dispatcher);

                    if (token == TokenState.Expired)
                        SendMessage(new StartShiftResponse(request.GUID, ResponseState.ExpiredToken, false));
                    else if (token == TokenState.UnsufficentRights)
                        SendMessage(new StartShiftResponse(request.GUID, ResponseState.UnsufficentRights, false));
                    else if (token == TokenState.Ok)
                    {
                        if (user is null)
                        {
                            SendMessage(new StartShiftResponse(request.GUID, ResponseState.InvalidToken, false));
                            return;
                        }

                        Client? client = database.Routes
                            .Include(x => x.Users).Include(x => x.Clients).ThenInclude(x => x.Stations)
                            .FirstOrDefault(x => x.Users.Contains(user) && x.Clients.Any(x => x.Id == request.ClientId))?.Clients.SingleOrDefault(x => x.Id == request.ClientId);

                        if (client is null)
                        {
                            SendMessage(new StartShiftResponse(request.GUID, ResponseState.UnsufficentRights, false));
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
                            this.client.Shift = shift;
                            if (SendMessage(new StartShiftResponse(request.GUID, ResponseState.Success, true, shift.Id)))
                                Worker.Logger.LogInformation($"[{Id}] Shift {shift.Id} started.");
                        }
                        else
                        {
                            SendMessage(new StartShiftResponse(request.GUID, ResponseState.Success, false));
                        }
                    }
                    else
                        SendMessage(new StartShiftResponse(request.GUID, ResponseState.InvalidToken, false));
                }
            }
        }

        private void ProcessClientDataRequest(string data)
        {
            Worker.Logger.LogInformation($"[{Id}] Client data requested");
            ClientDataRequest? request = JsonConvert.DeserializeObject<ClientDataRequest>(data);

            if (request is not null)
            {
                if (SendMessage(ServerWorker.GetClientData(request)))
                    Worker.Logger.LogInformation($"[{Id}] Client data sent");
            }
        }

        private void ProcessPing(string data)
        {
            Ping? request = JsonConvert.DeserializeObject<Ping>(data);
            if (request is not null)
            {
                SendMessage(new Pong(request.GUID));
            }
        }

#endregion

        private void EndShift()
        {
            
            if (client?.Shift is not null)
            {
                using (Context context = new Context())
                {
                    Shift shift = context.Shifts.First(x => x.Id == client.Shift.Id);
                    shift.EndTime = DateTime.Now;
                    context.SaveChanges();
                    client.Shift = null;
                    Worker.Logger.LogInformation($"[{Id}] Shift {shift.Id} ended.");
                }
            }
        }
    }
}
