using Communication.Procedures;
using Communication.Procedures.Basic;
using Communication.Procedures.Clients;
using Communication.Procedures.Users;
using TcpClient = NetCoreServer.TcpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public delegate void MessageReceivedEventHandler(Procedure procedure);

    public class EDDClient : TcpClient
    {
        List<MessageData> sentMessages = new();

        bool handshakeReceived = false;

        public event MessageReceivedEventHandler MessageReceived;

        public EDDClient(string address, int port) : base(address, port) { }

        protected override void OnConnected()
        {
            
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);

            if (string.IsNullOrWhiteSpace(message))
                return;

            VoidProcedure? voidProc;
            try
            {
                voidProc = JsonConvert.DeserializeObject<VoidProcedure>(message);
            }
            catch { return; }

            if (voidProc is not null)
            {
                if (voidProc.Type == ProcedureType.Ping)
                {
                    Send(JsonConvert.SerializeObject(new Pong(voidProc.GUID)));
                }
                else
                {
                    Procedure? p = voidProc;

                    switch (voidProc.Type)
                    {
                        case ProcedureType.LoginResponse:
                            p = JsonConvert.DeserializeObject<LoginResponse>(message);
                            break;
                        case ProcedureType.ClientsListResponse:
                            p = JsonConvert.DeserializeObject<ClientsListResponse>(message);
                            break;
                        case ProcedureType.StartShiftResponse:
                            p = JsonConvert.DeserializeObject<StartShiftResponse>(message);
                            break;
                        case ProcedureType.ClientDataResponse:
                            p = JsonConvert.DeserializeObject<ClientDataResponse>(message);
                            break;
                    }

                    if (p is not null)
                        MessageReceived?.Invoke(p);

                    if (message.Contains("RequestGUID"))
                    {
                        VoidResponse? response = JsonConvert.DeserializeObject<VoidResponse>(message);
                        if (response is not null)
                            sentMessages.RemoveAll(x => x.Message.GUID == response?.RequestGUID);
                    }
                }
            }
        }

        protected override void OnDisconnected()
        {
            handshakeReceived = false;
            // TODO: Restore connection
        }

        public bool SendMessage(Procedure proc)
        {
            if (IsConnected)
            {
                Send(JsonConvert.SerializeObject(proc));
                sentMessages.Add(new MessageData(proc));
                return true;
            }
            return false;
        }
    }

    public struct MessageData
    {
        public DateTime Sent { get; set; }

        public Procedure Message { get; set; }

        public MessageData(Procedure message)
        {
            Sent = DateTime.Now;
            Message = message;
        }
    }

    public enum ClientType
    {
        Client,
        Manager
    }
}
