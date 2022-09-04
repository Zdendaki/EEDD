using Communication.Procedures;
using Communication.Procedures.Basic;
using Communication.Procedures.Clients;
using Communication.Procedures.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using T = System.Timers;
using NetCoreServer;

namespace Communication
{
    public delegate void MessageReceivedEventHandler(Procedure procedure);

    public class EDDClient : WsClient
    {
        List<MessageData> sentMessages = new();

        DiffieHellman diffie;
        DiffieHellman.AesKeys aesKeys;
        byte[] publicKey;
        bool handshaked;
        T.Timer timer;

        public bool Encryption { get; set; }

        public event MessageReceivedEventHandler MessageReceived;

        public EDDClient(string address, int port, bool encryption = true) : base(address, port)
        {
            diffie = new DiffieHellman(encryption);
            timer = new(5000);
            handshaked = false;
            publicKey = new byte[158];
            timer.Elapsed += Timer_Elapsed;
            Encryption = encryption;
        }

        private void Timer_Elapsed(object? sender, T.ElapsedEventArgs e)
        {
            lock (timer)
            {
                if (sentMessages.Any(x => (DateTime.Now - x.Sent).TotalSeconds > 10d))
                {
                    foreach (MessageData message in sentMessages.Where(x => (DateTime.Now - x.Sent).TotalSeconds > 10d).OrderBy(x => x.Sent))
                    {
                        sentMessages.Remove(message);
                        SendMessage(message.Message);
                        Thread.Sleep(50);
                    }
                }
            }
        }

        public override void OnWsConnecting(HttpRequest request)
        {
            request.SetBegin("GET", "/");
            request.SetHeader("Host", "localhost");
            request.SetHeader("Origin", "http://localhost");
            request.SetHeader("Upgrade", "websocket");
            request.SetHeader("Connection", "Upgrade");
            request.SetHeader("Sec-WebSocket-Key", Convert.ToBase64String(WsNonce));
            request.SetHeader("Sec-WebSocket-Protocol", "eedd");
            request.SetHeader("Sec-WebSocket-Version", "13");
            request.SetBody();
        }

        public override void OnWsConnected(HttpResponse response)
        {
            timer.Enabled = true;

            if (Encryption)
            {
                SendTextAsync(Utils.Combine(DiffieHellman.Handshake, diffie.PublicKey));

                Task.Run(() =>
                {
                    Thread.Sleep(500);
                    
                });
            }
        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            byte[] message = buffer.Cut(offset, size);

            if (Encryption && !handshaked && buffer.LongLength == 166L && buffer.StartsWith(DiffieHellman.Handshake))
            {
                Array.Copy(buffer, 8, publicKey, 0, 158);
                aesKeys = diffie.GenerateKeys(publicKey);
                handshaked = true;

                return;
            }

            if (handshaked || !Encryption)
                ProcessMessage(message);
        }

        private void ProcessMessage(byte[] buffer)
        {
            string message = "";
            VoidProcedure? voidProc;
            try
            {
                message = DecryptMessage(buffer).Trim();

                if (string.IsNullOrWhiteSpace(message))
                    return;

                voidProc = JsonConvert.DeserializeObject<VoidProcedure>(message);
            }
            catch { Console.WriteLine(message); return; }

            if (voidProc is not null)
            {
                if (voidProc.Type == ProcedureType.Ping)
                {
                    SendMessage(new Pong(voidProc.GUID));
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
                            sentMessages.RemoveAll(x => Utils.Compare(x.Message.GUID, response?.RequestGUID));
                    }
                }
            }
        }

        public override void OnWsDisconnected()
        {
            // TODO: Restore connection
            timer.Enabled = false;
        }

        public bool SendMessage(Procedure proc, int tries = 1)
        {
            if (IsConnected && (handshaked || !Encryption))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(proc));
                byte[] output = diffie.Encrypt(buffer, aesKeys);
                bool sent = SendBinaryAsync(output);
                if (proc is not IResponse && proc is not Ping)
                    sentMessages.Add(new MessageData(proc, tries));
                return sent;
            }
            return false;
        }

        public string DecryptMessage(byte[] data)
        {
            return Encoding.UTF8.GetString(diffie.Decrypt(data, aesKeys));
        }
    }

    public enum ClientType
    {
        Client,
        Manager
    }
}
