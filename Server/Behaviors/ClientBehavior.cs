using Communication.Procedures;
using Communication.Procedures.Clients;
using Communication.Procedures.Users;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServerData;
using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server.Behaviors
{
    internal class ClientBehavior : WebSocketBehavior
    {
        User? userData = null;
        Shift? userShift = null;
        
        protected override void OnOpen()
        {
            Worker.Logger.LogInformation("Client connected");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            VoidProcedure? procedure;
            try
            {
                procedure = JsonConvert.DeserializeObject<VoidProcedure>(e.Data);
            }
            catch { return; }

            if (procedure is not null)
            {
                switch (procedure.Type)
                {
                    case ProcedureType.LoginRequest:
                        ProcessLogin(e.Data);
                        break;
                    case ProcedureType.ClientsListRequest:
                        ProcessClientsListRequest(e.Data);
                        break;
                    case ProcedureType.StartShiftRequest:
                        ProcessStartShiftRequest(e.Data);
                        break;
                    case ProcedureType.ClientDataRequest:
                        ProcessClientDataRequest(e.Data);
                        break;
                }
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            EndShift();
            Worker.Logger.LogInformation("Client disconnected");
        }

        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            
        }

        private void ProcessLogin(string data)
        {
            Worker.Logger.LogInformation("Login requested");
            LoginRequest? request = JsonConvert.DeserializeObject<LoginRequest>(data);
            if (request is not null)
            {
                var (res, u) = ServerWorker.DoLogin(request, UserRole.User);
                userData = u;
                Send(JsonConvert.SerializeObject(res));
                Worker.Logger.LogInformation("Login response sent");
            }
        }

        private void ProcessClientsListRequest(string data)
        {
            ClientsListRequest? request = JsonConvert.DeserializeObject<ClientsListRequest>(data);

            if (request is not null)
            {
                Send(JsonConvert.SerializeObject(ServerWorker.GetClients(request)));
            }
        }

        private void ProcessStartShiftRequest(string data)
        {
            using (Context database = new Context())
            {
                StartShiftRequest? request = JsonConvert.DeserializeObject<StartShiftRequest>(data);

                if (request is not null)
                {
                    (TokenState token, User? user) = database.CheckUser(request.Token, UserRole.User);

                    if (token == TokenState.Expired)
                        Send(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.ExpiredToken, false)));
                    else if (token == TokenState.UnsufficentRights)
                        Send(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.UnsufficentRights, false)));
                    else if (token == TokenState.Ok)
                    {
                        if (user is null)
                        {
                            Send(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.InvalidToken, false)));
                            return;
                        }

                        int? clientId = database.Routes.Include(x => x.Users).Include(x => x.Clients).FirstOrDefault(x => x.Users.Contains(user) && x.Clients.Any(x => x.Id == request.ClientId))?.Clients.FirstOrDefault(x => x.Id == request.ClientId)?.Id;
                        if (clientId is null)
                        {
                            Send(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.UnsufficentRights, false)));
                            return;
                        }

                        Client client = database.Clients.Include(x => x.Stations).Include(x => x.Shifts).First(x => x.Id == clientId);
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
                            Send(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.Success, true, sh.Entity.Id)));
                        }
                        else
                        {
                            Send(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.Success, false)));
                        }
                    }
                    else
                        Send(JsonConvert.SerializeObject(new StartShiftResponse(request.GUID, ResponseState.InvalidToken, false)));
                }
            }
        }

        private void ProcessClientDataRequest(string data)
        {
            using (Context database = new Context())
            {
                ClientDataRequest? request = JsonConvert.DeserializeObject<ClientDataRequest>(data);

                if (request is not null)
                {

                }
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
