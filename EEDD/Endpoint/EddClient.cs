using Common;
using Common.Data;
using Common.Messages;
using Common.Messages.Data;
using MessagePack;
using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using SocketError = System.Net.Sockets.SocketError;
using Timer = System.Timers.Timer;

namespace EEDD.Endpoint
{
    public delegate void MessageReceivedEventHandler(Message message);

    internal class EddClient : TcpClient
    {
        byte _errorCounter = 0;
        bool _stop = false;
        List<MessageCache> _sentMessages = [];
        Timer _timer;

        public event MessageReceivedEventHandler? MessageReceived;

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

        }

        protected override void OnDisconnected()
        {
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
            _sentMessages.Add(new(message, callback));
            return SendMessage(message);
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

            OnMessageReceived(message);

            switch (message)
            {
                case ResponseMessage response:
                    ParseResponse(response);
                    break;
                case RoutesMessage routes:
                    UpdateRoutes(routes.Routes);
                    break;
                case RouteDataMessage routeData:
                    SetRouteData(routeData);
                    break;
            }
        }

        protected virtual void OnMessageReceived(Message message)
        {
            MessageReceived?.Invoke(message);
        }

        private void UpdateRoutes(IEnumerable<RouteBase> routes)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                App.Routes.Clear();
                foreach (RouteBase route in routes)
                    App.Routes.Add(route);
            });
        }

        private void SetRouteData(RouteDataMessage routeData)
        {

        }

        private void ParseResponse(ResponseMessage response)
        {
            if (_sentMessages.FirstOrDefault(x => x.Message.ID == response.RequestID) is MessageCache cache)
            {
                cache.Callback?.Invoke(response);
                _sentMessages.Remove(cache);
            }
        }
    }
}
