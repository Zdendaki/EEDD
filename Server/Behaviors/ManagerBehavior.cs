using Communication.Procedures;
using Communication.Procedures.Users;
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
    internal class ManagerBehavior : WebSocketBehavior
    {
        User? userData;
        
        protected override void OnOpen()
        {
            Worker.Logger.LogInformation("Manager connected");
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
                }
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Worker.Logger.LogInformation("Manager disconnected");
        }

        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {

        }

        private void ProcessLogin(string data)
        {
            LoginRequest? request = JsonConvert.DeserializeObject<LoginRequest>(data);
            if (request is not null)
            {
                var response = ServerWorker.DoLogin(request, UserRole.Manager);
                userData = response.u;
                Send(JsonConvert.SerializeObject(response.res));
            }
        }
    }
}
