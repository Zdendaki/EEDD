using NetCoreServer;
using System.Net;
using System.Net.Sockets;

namespace Server.Endpoints
{
    internal class EddServer : TcpServer
    {
        private readonly ILogger<EddServer> _logger;
        private readonly IServiceProvider _provider;

        public EddServer(IConfiguration config, ILogger<EddServer> logger, IServiceProvider provider) : base(IPAddress.Any, ushort.Parse(config["Port"]!))
        {
            _logger = logger;
            _provider = provider;

            OptionKeepAlive = true;
            OptionTcpKeepAliveInterval = 2;
            OptionTcpKeepAliveRetryCount = 5;
            OptionTcpKeepAliveTime = 60;
        }

        protected override TcpSession CreateSession()
        {
            return ActivatorUtilities.CreateInstance<EddSession>(_provider);
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"EDD Server error: {error}");
        }

        public EddSession? FindSession()
        {
            return null;
        }
    }
}
