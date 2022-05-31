using Communication;
using Communication.Data;
using Communication.Procedures;
using Communication.Procedures.Clients;
using Communication.Procedures.Users;
using Microsoft.EntityFrameworkCore;
using NetCoreServer;
using Newtonsoft.Json;
using ServerData;
using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.TCP
{
    internal class EDDSession : TcpSession
    {
        List<TCPError> Errors { get; set; }
        
        readonly DiffieHellman diffie;
        DiffieHellman.AesKeys aesKeys;

        User? userData = null;
        Shift? userShift = null;
        byte[] publicKey = new byte[158];
        bool handshaked = false;

        public EDDSession(TcpServer server) : base (server)
        {
            diffie = ((TCPServer)Server).Diffie;
            Errors = new();
        }

        protected override void OnConnected()
        {
            Worker.Logger.LogInformation($"[{Id}] Client connected");
            SendAsync(Utils.Combine(DiffieHellman.Handshake, diffie.PublicKey));
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            if (size == 166 && buffer.StartsWith((int)offset, DiffieHellman.Handshake))
            {
                Array.Copy(buffer, offset + 8, publicKey, 0, 158);
                handshaked = true;
                aesKeys = diffie.GenerateKeys(publicKey);
                Worker.Logger.LogDebug($"[{Id}] Client handshaked");
                return;
            }

            if (!handshaked)
            {
                Worker.Logger.LogWarning($"[{Id}] Client requested message without handshake");
                return;
            }

            string message;
            VoidProcedure? procedure;
            try
            {
                message = DecryptMessage(buffer, offset, size);

                if (string.IsNullOrWhiteSpace(message))
                    return;

                procedure = JsonConvert.DeserializeObject<VoidProcedure>(message);
            }
            catch { return; }


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
                }
            }
        }

        protected override void OnDisconnected()
        {
            EndShift();
            Worker.Logger.LogInformation($"[{Id}] Client disconnected");
        }

        protected override void OnError(SocketError error)
        {
            Errors.Add(new(error));
        }

        internal bool SendMessageAsync(object? message)
        {
            if (handshaked)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                return SendAsync(diffie.Encrypt(buffer, aesKeys));
            }
            else
                return false;
        }

        internal string DecryptMessage(byte[] data, long offset, long size)
        {
            byte[] buffer = new byte[size];
            Array.Copy(data, offset, buffer, 0, size);

            return Encoding.UTF8.GetString(diffie.Decrypt(buffer, aesKeys));
        }

        private void ProcessLogin(string data)
        {
            Worker.Logger.LogInformation($"[{Id}] Login requested");
            LoginRequest? request = JsonConvert.DeserializeObject<LoginRequest>(data);
            if (request is not null)
            {
                var (res, u) = ServerWorker.DoLogin(request, UserRole.Dispatcher);
                userData = u;
                SendAsync(JsonConvert.SerializeObject(res));
                Worker.Logger.LogInformation($"[{Id}] Login response sent");
            }
        }

        private void ProcessClientsListRequest(string data)
        {
            ClientsListRequest? request = JsonConvert.DeserializeObject<ClientsListRequest>(data);

            if (request is not null)
            {
                SendAsync(JsonConvert.SerializeObject(ServerWorker.GetClients(request)));
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
                        SendAsync(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.ExpiredToken, false)));
                    else if (token == TokenState.UnsufficentRights)
                        SendAsync(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.UnsufficentRights, false)));
                    else if (token == TokenState.Ok)
                    {
                        if (user is null)
                        {
                            SendAsync(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.InvalidToken, false)));
                            return;
                        }

                        Client? client = database.Routes
                            .Include(x => x.Users).Include(x => x.Clients).ThenInclude(x => x.Stations)
                            .FirstOrDefault(x => x.Users.Contains(user) && x.Clients.Any(x => x.Id == request.ClientId))?.Clients.SingleOrDefault(x => x.Id == request.ClientId);

                        if (client is null)
                        {
                            SendAsync(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.UnsufficentRights, false)));
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
                            SendAsync(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.Success, true, sh.Entity.Id)));
                        }
                        else
                        {
                            SendAsync(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.Success, false)));
                        }
                    }
                    else
                        SendAsync(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.InvalidToken, false)));
                }
            }
        }

        private void ProcessClientDataRequest(string data)
        {
            ClientDataRequest? request = JsonConvert.DeserializeObject<ClientDataRequest>(data);

            if (request is not null)
            {
                SendAsync(JsonConvert.SerializeObject(ServerWorker.GetClientData(request)));
            }
        }

        private void EndShift()
        {
            if (userShift is not null)
            {
                using (Context context = new Context())
                {
                    Shift shift = context.Shifts.First(x => x.Id == userShift.Id);
                    shift.EndTime = DateTime.Now;
                    context.SaveChanges();
                    userShift = null;
                }
            }
        }
    }
}
