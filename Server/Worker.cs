using Server.Endpoints;

namespace Server
{
    internal class Worker : TimedBackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly EddServer _eddServer;
        private readonly EvalServer _evalServer;
        private readonly RuntimeData _data;

        public Worker(ILogger<Worker> log, IConfiguration config, EddServer eddServer, EvalServer evalServer, RuntimeData data) : base("EDD/EVAL Server", log, TimeSpan.FromSeconds(10))
        {
            _logger = log;
            _config = config;
            _eddServer = eddServer;
            _evalServer = evalServer;
            _data = data;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _data.LoadData();

            if (_eddServer.Start())
                _logger.Log(LogLevel.Information, $"EDD Server started on port {_eddServer.Port}.");

            if (_evalServer.Start())
                _logger.Log(LogLevel.Information, $"EVAL Server started on port {_evalServer.Port}.");

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_eddServer.Stop())
                _logger.Log(LogLevel.Information, "EDD Server stopped.");

            if (_evalServer.Stop())
                _logger.Log(LogLevel.Information, "EVAL Server stopped.");

            await base.StopAsync(cancellationToken);
        }

        protected override async Task DoWorkAsync()
        {

        }
    }
}
