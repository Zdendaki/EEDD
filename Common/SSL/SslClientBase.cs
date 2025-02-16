using Common.Messages;
using MessagePack;
using NetCoreServer;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Common.SSL
{
    public abstract class SslClientBase : SslClient, ISslClient
    {
        protected static readonly byte[] PUBLIC_KEY = GetPublicKey();

        private bool _stop = false;
        private int _errorCounter = 0;

        public event MessageReceivedEventHandler? MessageReceived;
        public event ConnectionChangedEventHandler? ConnectionChanged;

        protected event MessageReceivedEventHandler? MessageReceivedInternal;

        protected SslClientBase(IPAddress address, int port) : base(new(SslProtocols.Tls12), address, port)
        {
            OptionKeepAlive = true;
            OptionTcpKeepAliveInterval = 2;
            OptionTcpKeepAliveRetryCount = 5;
            OptionTcpKeepAliveTime = 60;

            Context.CertificateValidationCallback += ValidateCertificate;
        }

        private bool ValidateCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            if (certificate is null)
                return false;

            byte[] publicKey = certificate.GetPublicKey();

            return publicKey.SequenceEqual(PUBLIC_KEY);
        }

        private static byte[] GetPublicKey()
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Common.EDD.der")!;
            byte[] cert = new byte[stream.Length];

            for (int i = 0; i < stream.Length; i++)
                cert[i] = (byte)stream.ReadByte();

            using X509Certificate2 verify = X509CertificateLoader.LoadCertificate(cert);
            return verify.GetPublicKey();
        }

        protected override void OnConnected()
        {
            OnConnectionChanged(true);
        }

        protected override void OnDisconnected()
        {
            OnConnectionChanged(false);

            while (!_stop && !Connect())
            {
                Thread.Sleep(10000);
                Connect();
            }
        }

        public void DisconnectAndStop()
        {
            _stop = true;
            if (DisconnectAsync())
            {
                while (IsConnected)
                    Thread.Yield();
            }
        }

        public virtual bool SendMessage(Message message)
        {
            try
            {
                byte[] buffer = MessagePackSerializer.Serialize(message, MessagePackHelper.LZ4);
                return SendAsync(buffer);
            }
            catch
            {
                return false;
            }
        }

        public Task<ResponseMessage?> SendRequest(Message message, TimeSpan wait)
        {
            bool gotResponse = false;
            TaskCompletionSource<ResponseMessage?> tcs = new();
            Timer timer = new(_ => timeout(), null, (long)wait.TotalMilliseconds, Timeout.Infinite);

            void received(MessageReceivedEventArgs e)
            {
                if (e.Message is not ResponseMessage response)
                    return;

                if (response.RequestID == message.ID)
                {
                    gotResponse = true;
                    MessageReceivedInternal -= received;
                    e.Handled = true;
                    tcs.SetResult(response);
                }
            }

            void timeout()
            {
                if (gotResponse)
                    return;

                MessageReceivedInternal -= received;
                tcs.SetResult(null);
            }

            if (SendMessage(message))
            {
                MessageReceivedInternal += received;
            }
            else
            {
                tcs.SetResult(null);
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            return tcs.Task;
        }

        protected override sealed void OnReceived(byte[] buffer, long offset, long size)
        {
            Receive(buffer.AsMemory((int)offset, (int)size));
        }

        private void Receive(ReadOnlyMemory<byte> buffer)
        {
            Message message;

            try
            {
                message = MessagePackSerializer.Deserialize<Message>(buffer, MessagePackHelper.LZ4);
            }
            catch
            {
                _errorCounter++;

                if (_errorCounter > 100)
                    Disconnect();

                return;
            }

            MessageReceivedEventArgs e = new(message);
            OnMessageReceivedInternal(e);

            if (e.Handled)
                return;

            OnMessageReceived(e);

            if (e.Handled)
                return;

            Receive(message);
        }

        protected abstract void Receive(Message message);

        #region Event firing
        protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(e);
        }

        protected virtual void OnMessageReceivedInternal(MessageReceivedEventArgs e)
        {
            MessageReceivedInternal?.Invoke(e);
        }

        protected virtual void OnConnectionChanged(bool connected)
        {
            ConnectionChanged?.Invoke(connected);
        }
        #endregion
    }
}
