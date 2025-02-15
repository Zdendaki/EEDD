using Common;
using Common.Messages;
using Common.Messages.Data;
using MessagePack;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SocketError = System.Net.Sockets.SocketError;
using ThreadTimer = System.Threading.Timer;
using Timer = System.Timers.Timer;

namespace EEDD.Endpoint
{
    internal delegate void MessageReceivedEventHandler(MessageReceivedEventArgs e);
    internal delegate void ConnectionChangedEventHandler(bool connected);

    internal class EddClient : TcpClient
    {
        byte _errorCounter = 0;
        bool _stop = false;

        readonly object _sentMessagesLock = new();
        readonly List<MessageCache> _sentMessages = [];
        readonly Timer _timer;

        public event MessageReceivedEventHandler? MessageReceived;
        public event ConnectionChangedEventHandler? ConnectionChanged;

        protected event MessageReceivedEventHandler? MessageReceivedInternal;

        public EddClient(IPAddress address, ushort port) : base(address, port)
        {
            OptionKeepAlive = true;
            OptionTcpKeepAliveInterval = 2;
            OptionTcpKeepAliveRetryCount = 5;
            OptionTcpKeepAliveTime = 60;

            _timer = new(TimeSpan.FromSeconds(10));
            _timer.Enabled = true;
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {

        }

        public void DisconnectAndStop()
        {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
                Thread.Yield();
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

        protected override void OnError(SocketError error)
        {

        }

        public bool SendMessage(Message message)
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

        public bool SendQueuedMessage(Message message, ResponseCallback? callback)
        {
            lock (_sentMessages)
            {
                _sentMessages.Add(new(message, callback));
            }

            return SendMessage(message);
        }

        public Task<ResponseMessage?> SendRequest(Message message, TimeSpan wait)
        {
            bool gotResponse = false;
            TaskCompletionSource<ResponseMessage?> tcs = new();
            ThreadTimer timer = new(_ => timeout(), null, (long)wait.TotalMilliseconds, Timeout.Infinite);

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

        protected override void OnReceived(byte[] buffer, long offset, long size)
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

            switch (message)
            {
                case ResponseMessage response:
                    ParseResponse(response);
                    break;
                case RouteDataMessage routeData:
                    SetRouteData(routeData);
                    break;
            }
        }

        private void ParseResponse(ResponseMessage response)
        {
            lock (_sentMessages)
            {
                if (_sentMessages.FirstOrDefault(x => x.Message.ID == response.RequestID) is MessageCache cache)
                {
                    cache.Callback?.Invoke(response);
                    _sentMessages.Remove(cache);
                }
            }
        }

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

        private void SetRouteData(RouteDataMessage routeData)
        {
            App.Route = routeData.Route;
        }
        #endregion
    }
}
