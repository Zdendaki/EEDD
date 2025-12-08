using Common.Messages;

namespace Common.TCP
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public Message Message { get; }

        public bool Handled { get; set; }

        public MessageReceivedEventArgs(Message message)
        {
            Message = message;
            Handled = false;
        }
    }
}
