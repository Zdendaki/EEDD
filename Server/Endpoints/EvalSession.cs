using Common.Messages;

namespace Server.Endpoints
{
    internal class EvalSession : SslSessionBase
    {
        private readonly RuntimeData _data;

        public EvalSession(EvalServer server, ILogger<EvalSession> logger, RuntimeData data) : base(server, logger)
        {
            _data = data;
        }

        protected override void Receive(Message message)
        {

        }
    }
}
