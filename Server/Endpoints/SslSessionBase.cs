using Common;
using Common.Messages;
using MessagePack;
using NetCoreServer;

namespace Server.Endpoints
{
    internal abstract class SslSessionBase : SslSession
    {
        protected readonly ILogger Logger;

        private int _errorCounter = 0;

        protected SslSessionBase(SslServer server, ILogger logger) : base(server)
        {
            Logger = logger;
        }

        protected override void OnConnected()
        {
            Logger.LogInformation($"[{Id}] Connected.");
        }

        protected override void OnHandshaked()
        {
            Logger.LogInformation($"[{Id}] Handshaked.");
        }

        protected override void OnDisconnected()
        {
            Logger.LogInformation($"[{Id}] Disconnected.");
        }

        public bool SendMessage(Message message)
        {
            try
            {
                byte[] buffer = MessagePackSerializer.Serialize(message, MessagePackHelper.LZ4);
                return SendAsync(buffer);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[{Id}] Failed to send message.");
                return false;
            }
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            Logger.LogDebug($"[{Id}] Received {size} bytes of data.");
            ReceiveBuffer(buffer.AsMemory((int)offset, (int)size));
        }

        private void ReceiveBuffer(ReadOnlyMemory<byte> buffer)
        {
            Message message;

            try
            {
                message = MessagePackSerializer.Deserialize<Message>(buffer, MessagePackHelper.LZ4);
            }
            catch
            {
                _errorCounter++;
                if (_errorCounter > 100)
                    Disconnect();
                return;
            }

            Receive(message);
        }

        protected abstract void Receive(Message message);
    }
}
