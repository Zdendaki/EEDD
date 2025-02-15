using Common.Messages;
using MessagePack;
using NetCoreServer;
using System.Net;

namespace Common.TCP
{
    public abstract class TcpClientBase : TcpClient, ITcpClient
    {
        private bool _stop = false;
        private int _errorCounter = 0;

        public event MessageReceivedEventHandler? MessageReceived;
        public event ConnectionChangedEventHandler? ConnectionChanged;

        protected event MessageReceivedEventHandler? MessageReceivedInternal;

        protected TcpClientBase(IPAddress address, int port) : base(address, port)
        {
            OptionKeepAlive = true;
            OptionTcpKeepAliveInterval = 2;
            OptionTcpKeepAliveRetryCount = 5;
            OptionTcpKeepAliveTime = 60;
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
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
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
