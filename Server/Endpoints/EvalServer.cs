using System.Net.Sockets;

namespace Server.Endpoints
{
    internal class EvalServer : SslServerBase<EvalSession>
    {
        private readonly ILogger<EvalServer> _logger;

        public EvalServer(IConfiguration config, ILogger<EvalServer> logger, IServiceProvider provider) : base(provider, config, ushort.Parse(config["PortEVAL"]!))
        {
            _logger = logger;
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"EVAL Server error: {error}");
        }

        public override EvalSession? FindSession()
        {
            return null;
        }
    }
}
