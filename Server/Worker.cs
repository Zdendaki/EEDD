using Server.TCP;
using ServerData.Database;
using System.Net;

namespace Server
{
    public class Worker : BackgroundService
    {
        internal static ILogger<Worker> Logger { get; private set; }

        internal static List<ClientInfo> Clients { get; } = new();

        TCPServer server;
        bool criticalError = false;

        public Worker(ILogger<Worker> logger)
        {
            Logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                InitServer();
            }
            catch (Exception e)
            {
                Logger.LogCritical("Couldn't start TCP service. Exception: " + e.Message);
                criticalError = true;
                return base.StartAsync(cancellationToken);
            }

            #pragma warning disable CS8600
            Context context = null;
            #pragma warning restore CS8600
            try
            {
                context = new Context();
            }
            catch (Exception e)
            {
                Logger.LogCritical("Couldn't connect to database. Stopping TCP service. Exception: " + e.Message);
                server.Stop();
                criticalError = true;
            }
            finally
            {
                context?.Dispose();
            }

            Logger.LogInformation("Server started successfully.");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!server.IsStarted && !criticalError)
                {
                    Logger.LogWarning("TCP service isn't listening, trying to start again.");
                    try
                    {
                        server.Start();
                    }
                    catch
                    {
                        Logger.LogError("Couldn't start TCP service. Performing hard restart of TCP service.");
                        try
                        {
                            try
                            {
                                server.Stop();
                            }
                            catch { }

                            InitServer();
                        }
                        catch
                        {
                            criticalError = true;
                            Logger.LogCritical("Couldn't start TCP service after hard reset.");
                        }
                    }
                }
                else
                {
                    Logger.LogInformation("TCP service is OK");
                }

                await Task.Delay(60000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            server.Stop();
            return base.StopAsync(cancellationToken);
        }

        private void InitServer()
        {
            Logger.LogInformation("Starting TCP service");
            int port = 9180;
            server = new TCPServer(IPAddress.Any, port);
            server.Start();
            Logger.LogInformation("TCP service started on port " + port);
        }
    }
}