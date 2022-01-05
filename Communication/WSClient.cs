using Communication.Procedures;
using Communication.Procedures.Basic;
using Communication.Procedures.Clients;
using Communication.Procedures.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Communication
{
    public delegate void MessageReceivedEventHandler(Procedure procedure);

    public class WSClient
    {
        WebSocket client;
        List<MessageData> sentMessages;

        public bool Connected { get => client?.ReadyState == WebSocketState.Open; }

        public event MessageReceivedEventHandler MessageReceived;

        public WSClient(ClientType type)
        {
            string clientType;
            switch (type)
            {
                case ClientType.Client:
                    clientType = "/Client";
                    break;
                case ClientType.Manager:
                    clientType = "/Manager";
                    break;
                default:
                    clientType = "/";
                    break;
            }

            sentMessages = new();
            client = new WebSocket("ws://localhost:9180" + clientType);
            client.Compression = CompressionMethod.Deflate;
            client.OnMessage += ReceiveMessage;
        }

        private void ReceiveMessage(object? sender, MessageEventArgs e)
        {
            if (!e.IsText)
                return;
            
            VoidProcedure? voidProc;
            try
            {
                voidProc = JsonConvert.DeserializeObject<VoidProcedure>(e.Data);
            }
            catch { return; }

            if (voidProc is not null)
            {
                if (voidProc.Type == ProcedureType.Ping)
                {
                    client.Send(JsonConvert.SerializeObject(new Pong(voidProc.GUID)));
                }
                else
                {
                    Procedure? p = voidProc;

                    switch (voidProc.Type)
                    {
                        case ProcedureType.LoginResponse:
                            p = JsonConvert.DeserializeObject<LoginResponse>(e.Data);
                            break;
                        case ProcedureType.ClientsListResponse:
                            p = JsonConvert.DeserializeObject<ClientsListResponse>(e.Data);
                            break;
                        case ProcedureType.StartShiftResponse:
                            p = JsonConvert.DeserializeObject<StartShiftResponse>(e.Data);
                            break;
                    }

                    if (p is not null)
                        MessageReceived?.Invoke(p);

                    if (e.Data.Contains("RequestGUID"))
                    {
                        VoidResponse? response = JsonConvert.DeserializeObject<VoidResponse>(e.Data);
                        if (response is not null)
                            sentMessages.RemoveAll(x => x.Message.GUID == response?.RequestGUID);
                    }
                }
            }
        }

        public bool SendMessage(Procedure proc)
        {
            if (Connected)
            {
                client.Send(JsonConvert.SerializeObject(proc));
                sentMessages.Add(new MessageData(proc));
                return true;
            }
            return false;
        }

        public void Connect()
        {
            client.Connect();
        }

        public void Disconnect()
        {
            client.Close();
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
