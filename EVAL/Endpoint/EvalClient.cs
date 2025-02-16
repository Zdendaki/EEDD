using Common.Messages;
using Common.SSL;
using System.Net;

namespace EVAL.Endpoint
{
    internal class EvalClient : SslClientBase
    {
        public EvalClient(IPAddress address, int port) : base(address, port)
        {
        }

        protected override void Receive(Message message)
        {

        }
    }
}
