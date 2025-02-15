using System.Net.Sockets;

namespace Server.Endpoints
{
    internal class EddServer : TcpServerBase<EddSession>
    {
        private readonly ILogger<EddServer> _logger;

        public EddServer(IConfiguration config, ILogger<EddServer> logger, IServiceProvider provider) : base(provider, ushort.Parse(config["PortEDD"]!))
        {
            _logger = logger;
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"EDD Server error: {error}");
        }

        public override EddSession? FindSession()
        {
            return null;
        }
    }
}
