using Common.Data;
using Common.Messages;
using Common.Messages.Data;
using Common.Messages.Login;
using System.Net.Sockets;

namespace Server.Endpoints
{
    internal class EvalSession : SslSessionBase
    {
        private readonly RuntimeData _data;

        internal Route? Route { get; private set; }

        internal User? User { get; private set; }

        internal Guid? Secret { get; private set; }

        public EvalSession(EvalServer server, ILogger<EvalSession> logger, RuntimeData data) : base(server, logger)
        {
            _data = data;
        }

        protected override void Receive(Message message)
        {
            switch (message)
            {
                case DataRequestMessage dataRequest:
                    DataRequest(dataRequest);
                    break;
                case LoginMessage login:
                    Login(login);
                    break;
            }
        }

        private void DataRequest(DataRequestMessage request)
        {
            if (request.DataType == DataType.RoutesList)
            {
                RoutesMessage routes = new RoutesMessage(_data.Routes);
                SendMessage(routes);
            }
            else if (request.DataType == DataType.Route)
            {
                if (Route is null)
                {
                    SendMessage(ResponseMessage.GetUnauthorizedMessage(request.ID));
                    return;
                }

                RouteDataMessage routeData = new(_data.Routes.Single(x => x.ID == Route.ID));
                SendMessage(routeData);
            }
        }

        private void Login(LoginMessage login)
        {
            if (_data.Routes.FirstOrDefault(x => x.ID == login.RouteID) is not Route route || route.PasswordEDD != login.Password)
            {
                Logger.LogInformation($"[{Id}] Login unsuccessful.");
                SendMessage(ResponseMessage.GetBadCredentialsMessage(login.ID));
                return;
            }

            User user = login.GetUser();
            Guid secret = Guid.NewGuid();
            LoginResponseMessage response = new(login.ID, user, secret);

            if (SendMessage(response))
            {
                Route = route;
                User = user;
                Secret = secret;

                Logger.LogInformation($"[{Id}] Logged in as user {User.Name} ({User.DeviceID:X}) to route {route.Name}.");
            }
        }
    }
}
