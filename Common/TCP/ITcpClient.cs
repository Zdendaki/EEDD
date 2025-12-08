using Common.Messages;

namespace Common.TCP
{
    public interface ITcpClient
    {
        void DisconnectAndStop();

        bool SendMessage(Message message);

        Task<ResponseMessage?> SendRequest(Message message, TimeSpan wait);
    }
}
