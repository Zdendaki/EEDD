using NetCoreServer;
using System.Net;

namespace Server.Endpoints
{
    internal abstract class TcpServerBase<TSession> : TcpServer where TSession : TcpSession
    {
        protected readonly IServiceProvider Provider;

        protected TcpServerBase(IServiceProvider provider, ushort port) : base(IPAddress.Any, port)
        {
            OptionKeepAlive = true;
            OptionTcpKeepAliveInterval = 2;
            OptionTcpKeepAliveRetryCount = 5;
            OptionTcpKeepAliveTime = 60;

            Provider = provider;
        }

        public abstract TSession? FindSession();

        protected override TSession CreateSession()
        {
            return ActivatorUtilities.CreateInstance<TSession>(Provider);
        }
    }
}
