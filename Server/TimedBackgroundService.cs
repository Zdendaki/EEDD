namespace Server
{
    internal abstract class TimedBackgroundService : BackgroundService
    {
        protected ulong _executionCount;
        private readonly string _serviceName;
        private readonly TimeSpan _period;
        private readonly ILogger _log;

        public TimeSpan Period => _period;

        public TimedBackgroundService(string serviceName, ILogger log, TimeSpan period)
        {
            _serviceName = serviceName;
            _period = period;
            _log = log;
        }

        protected abstract Task DoWorkAsync();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _log.LogInformation($"{_serviceName} service is running.");
            await DoWorkAsync();

            using PeriodicTimer timer = new(_period);

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    Interlocked.Increment(ref _executionCount);
                    await DoWorkAsync();
                }
            }
            catch (OperationCanceledException)
            {
                _log.LogInformation($"{_serviceName} service is stopping.");
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex, $"{_serviceName} service has thrown an exception and will be restarted.");
                await Task.Delay(5000);
                await ExecuteAsync(stoppingToken);
            }
        }
    }
}
