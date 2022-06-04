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
using T = System.Timers;

namespace Communication
{
    public delegate void MessageReceivedEventHandler(Procedure procedure);

    public class EDDClient : TcpClient
    {
        List<MessageData> sentMessages = new();

        DiffieHellman diffie;
        DiffieHellman.AesKeys aesKeys;
        byte[] publicKey;
        bool handshaked;
        T.Timer timer;

        public event MessageReceivedEventHandler MessageReceived;

        public EDDClient(string address, int port) : base(address, port) 
        {
            diffie = new DiffieHellman();
            timer = new(5000);
            handshaked = false;
            publicKey = new byte[158];
            timer.Elapsed += Timer_Elapsed; ;
        }

        private void Timer_Elapsed(object? sender, T.ElapsedEventArgs e)
        {
            
        }

        protected override void OnConnected()
        {
            SendAsync(Utils.Combine(DiffieHellman.Handshake, diffie.PublicKey));
            timer.Enabled = true;
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            if (size == 166 && buffer.StartsWith((int)offset, DiffieHellman.Handshake))
            {
                Array.Copy(buffer, offset + 8, publicKey, 0, 158);
                aesKeys = diffie.GenerateKeys(publicKey);
                handshaked = true;

                return;
            }

            if (!handshaked)
                return;

            string message;
            VoidProcedure? voidProc;
            try
            {
                message = DecryptMessage(buffer, offset, size);

                if (string.IsNullOrWhiteSpace(message))
                    return;

                voidProc = JsonConvert.DeserializeObject<VoidProcedure>(message);
            }
            catch { return; }

            if (voidProc is not null)
            {
                if (voidProc.Type == ProcedureType.Ping)
                {
                    SendMessageAsync(new Pong(voidProc.GUID));
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
            // TODO: Restore connection
            timer.Enabled = false;
        }

        public bool SendMessageAsync(Procedure proc, int tries = 1)
        {
            if (IsConnected && handshaked)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(proc));
                
                bool flag = SendAsync(diffie.Encrypt(buffer, aesKeys));
                sentMessages.Add(new MessageData(proc, tries));
                return flag;
            }
            return false;
        }

        public string DecryptMessage(byte[] data, long offset, long size)
        {
            byte[] buffer = new byte[size];
            Array.Copy(data, offset, buffer, 0, size);

            return Encoding.UTF8.GetString(diffie.Decrypt(buffer, aesKeys));
        }
    }

    public enum ClientType
    {
        Client,
        Manager
    }
}
