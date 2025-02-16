using Common.Data;
using Common.Messages;
using Common.Messages.Data;
using Common.Messages.Login;
using Common.Messages.Response;
using Common.Messages.Train;

namespace Server.Endpoints
{
    internal class EddSession : SslSessionBase
    {
        private readonly RuntimeData _data;

        internal Route? Route { get; private set; }

        internal User? User { get; private set; }

        internal Guid? Secret { get; private set; }

        public EddSession(EddServer server, ILogger<EddSession> logger, RuntimeData data) : base(server, logger)
        {
            _data = data;
        }

        protected override void OnDisconnected()
        {
            base.OnDisconnected();

            if (Route is not null && User is not null)
            {
                foreach (var client in Route.Clients)
                {
                    if (client.User == User)
                    {
                        Logger.LogInformation($"[{Id}] Released user {User.Name} (ID {User.DeviceID:X}) from client {client.ID}: {client.Name}.");
                        client.User = null;
                        break;
                    }
                }
            }
        }

        protected override void Receive(Message message)
        {
            switch (message)
            {
                case ResponseMessage:
                    break;
                case DataRequestMessage dataRequest:
                    DataRequest(dataRequest);
                    break;
                case LoginMessage login:
                    Login(login);
                    break;
                case PodjRequest podjRequest:
                    PodjRequest(podjRequest);
                    break;
                case PodjResponse podjResponse:
                    PodjResponse(podjResponse);
                    break;
                case ClaimClientMessage claimClient:
                    ClaimStation(claimClient);
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

        private void PodjRequest(PodjRequest request)
        {

        }

        private void PodjResponse(PodjResponse response)
        {

        }

        private void ClaimStation(ClaimClientMessage claimClient)
        {
            if (Route is null || User is null)
            {
                SendMessage(ResponseMessage.GetUnauthorizedMessage(claimClient.ID));
                return;
            }

            if (Route.Clients.FirstOrDefault(x => x.ID == claimClient.ClientID) is not Client client)
            {
                SendMessage(StringResponseMessage.GetRefusedMessage(claimClient.ID, RefusedMessageHelper.NOTFOUND));
                return;
            }

            if (client.User is not null && client.User.ID != User.ID)
            {
                SendMessage(StringResponseMessage.GetRefusedMessage(claimClient.ID, RefusedMessageHelper.OCCUPIED));
                return;
            }

            client.User = User;

            Logger.LogInformation($"[{Id}] Claimed client {client.ID}: {client.Name}.");
            SendMessage(ResponseMessage.GetAcceptedMessage(claimClient.ID));
        }
    }
}
