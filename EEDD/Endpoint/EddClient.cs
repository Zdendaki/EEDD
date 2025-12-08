using Common.Messages;
using Common.Messages.Data;
using Common.TCP;
using System.Net;
using SocketError = System.Net.Sockets.SocketError;
using Timer = System.Timers.Timer;

namespace EEDD.Endpoint
{
    internal class EddClient : TcpClientBase
    {
        readonly object _sentMessagesLock = new();
        readonly List<MessageCache> _sentMessages = [];
        readonly Timer _timer;

        public EddClient(IPAddress address, ushort port) : base(address, port)
        {
            _timer = new(TimeSpan.FromSeconds(10));
            _timer.Enabled = true;
            _timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {

        }

        protected override void OnError(SocketError error)
        {

        }

        public bool SendQueuedMessage(Message message, ResponseCallback? callback)
        {
            lock (_sentMessages)
            {
                _sentMessages.Add(new(message, callback));
            }

            return SendMessage(message);
        }

        protected override void Receive(Message message)
        {
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
        private void SetRouteData(RouteDataMessage routeData)
        {
            App.Route = routeData.Route;
        }
    }
}
