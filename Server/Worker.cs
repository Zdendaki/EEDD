using Server.Behaviors;
using ServerData.Database;
using WebSocketSharp.Server;

namespace Server
{
    public class Worker : BackgroundService
    {
        internal static ILogger<Worker> Logger;
        internal static WebSocketServer Server;
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
                Logger.LogCritical("Couldn't start websocket server. Exception: " + e.Message);
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
                Logger.LogCritical("Couldn't connect to database. Stopping Websocket service. Exception: " + e.Message);
                Server.Stop();
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
                if (Server?.IsListening == false && !criticalError)
                {
                    Logger.LogWarning("Websocket service isn't listening, trying to start again.");
                    try
                    {
                        Server.Start();
                    }
                    catch
                    {
                        Logger.LogError("Couldn't start Websocket service. Performing hard restart of Websocket service.");
                        try
                        {
                            try
                            {
                                Server.Stop();
                            }
                            catch { }

                            InitServer();
                        }
                        catch
                        {
                            criticalError = true;
                            Logger.LogCritical("Couldn't start Websocket service after hard reset.");
                        }
                    }
                }
                else
                {
                    Logger.LogInformation("Websocket service is OK");
                }

                await Task.Delay(60000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Server.Stop();
            return base.StopAsync(cancellationToken);
        }

        private void InitServer()
        {
            Server = new WebSocketServer(9180);
            Server.AddWebSocketService<TestBehavior>("/Test");
            Server.AddWebSocketService<ClientBehavior>("/Client");
            Server.AddWebSocketService<ClientBehavior>("/Manager");
            Server.Start();
        }
    }
}