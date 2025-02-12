using Server.Endpoints;

namespace Server
{
    internal class Worker : TimedBackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly EddServer _server;
        private readonly RuntimeData _data;

        public Worker(ILogger<Worker> log, IConfiguration config, EddServer server, RuntimeData data) : base("EDD Server", log, TimeSpan.FromSeconds(10))
        {
            _logger = log;
            _config = config;
            _server = server;
            _data = data;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _data.LoadData();

            if (_server.Start())
                _logger.Log(LogLevel.Information, "EDD Server started.");

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_server.Stop())
                _logger.Log(LogLevel.Information, "EDD Server stopped.");

            await base.StopAsync(cancellationToken);
        }

        protected override async Task DoWorkAsync()
        {

        }
    }
}
