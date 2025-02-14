using Common;
using Common.Data;
using Common.Messages;
using Common.Messages.Data;
using Common.Messages.Login;
using Common.Messages.Train;
using MessagePack;
using NetCoreServer;

namespace Server.Endpoints
{
    internal class EddSession : TcpSession
    {
        private readonly ILogger<EddSession> _logger;
        private readonly RuntimeData _data;
        private byte _errorCounter = 0;

        internal Route? Route { get; private set; }

        internal User? User { get; private set; }

        public EddSession(EddServer server, ILogger<EddSession> logger, RuntimeData data) : base(server)
        {
            _logger = logger;
            _data = data;
        }

        protected override void OnConnected()
        {
            _logger.LogInformation($"[{Id}] Connected.");
        }

        protected override void OnDisconnected()
        {
            _logger.LogInformation($"[{Id}] Disconnected.");
        }

        public bool SendMessage(Message message)
        {
            try
            {
                byte[] buffer = MessagePackSerializer.Serialize(message, MessagePackHelper.LZ4);
                return SendAsync(buffer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{Id}] Failed to send message.");
                return false;
            }
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            _logger.LogDebug($"[{Id}] Received {size} bytes of data.");
            ReceiveBuffer(buffer.AsMemory((int)offset, (int)size));
        }

        private void ReceiveBuffer(ReadOnlyMemory<byte> buffer)
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
            if (_data.Routes.FirstOrDefault(x => x.ID == login.RouteID) is not Route route || route.Password != login.Password)
            {
                _logger.LogInformation($"[{Id}] Login unsuccessful.");
                SendMessage(ResponseMessage.GetBadCredentialsMessage(login.ID));
                return;
            }

            if (SendMessage(ResponseMessage.GetAcceptedMessage(login.ID)))
            {
                Route = route;
                User = login.GetUser();

                _logger.LogInformation($"[{Id}] Logged in as user {User.Name} ({User.ID:X}) to route {route.Name}.");
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
                SendMessage(ResponseMessage.GetRefusedMessage(claimClient.ID, RefusedMessageHelper.NOTFOUND));
                return;
            }

            if (client.User is not null)
            {
                SendMessage(ResponseMessage.GetRefusedMessage(claimClient.ID, RefusedMessageHelper.OCCUPIED));
                return;
            }

            client.User = User;
            Id.ToByteArray();
            _logger.LogInformation($"[{Id}] claimed client {client.ID}.");
            SendMessage(ResponseMessage.GetAcceptedMessage(claimClient.ID));
        }
    }
}
