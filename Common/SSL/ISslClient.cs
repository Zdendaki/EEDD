using Common.Messages;

namespace Common.SSL
{
    public interface ISslClient
    {
        void DisconnectAndStop();

        bool SendMessage(Message message);

        Task<ResponseMessage?> SendRequest(Message message, TimeSpan wait);
    }
}
