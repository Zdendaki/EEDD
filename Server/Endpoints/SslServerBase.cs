using NetCoreServer;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Server.Endpoints
{
    internal abstract class SslServerBase<TSession> : SslServer where TSession : SslSession
    {
        protected readonly IServiceProvider Provider;
        protected readonly IConfiguration Config;

        protected SslServerBase(IServiceProvider provider, IConfiguration config, ushort port) : base(new(SslProtocols.Tls12), IPAddress.Any, port)
        {
            OptionKeepAlive = true;
            OptionTcpKeepAliveInterval = 2;
            OptionTcpKeepAliveRetryCount = 5;
            OptionTcpKeepAliveTime = 60;

            Provider = provider;
            Config = config;

            Context.Certificate = LoadCertificate();
        }

        public abstract TSession? FindSession();

        protected virtual X509Certificate2 LoadCertificate()
        {
            string fileName = Path.Combine(AppContext.BaseDirectory, Config["Certificate"]!);
            return X509CertificateLoader.LoadPkcs12FromFile(fileName, Config["CertificatePassword"]!);
        }

        protected override TSession CreateSession()
        {
            return ActivatorUtilities.CreateInstance<TSession>(Provider);
        }
    }
}
