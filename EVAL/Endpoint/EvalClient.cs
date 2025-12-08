using Common.Messages;
using Common.TCP;
using System.Net;

namespace EVAL.Endpoint
{
    internal class EvalClient : TcpClientBase
    {
        public EvalClient(IPAddress address, int port) : base(address, port)
        {
        }

        protected override void Receive(Message message)
        {

        }
    }
}
