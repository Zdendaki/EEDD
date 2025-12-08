namespace Common.Messages
{
    public delegate void ResponseCallback(ResponseMessage response);

    public class MessageCache
    {
        public DateTime Enqueued { get; init; }

        public bool RepeatSending { get; init; }

        public int SendAttempts { get; set; }

        public Message Message { get; init; }

        public ResponseCallback? Callback { get; init; }

        public MessageCache(Message message, ResponseCallback? callback, bool repeatSending = true)
        {
            Enqueued = DateTime.Now;
            RepeatSending = repeatSending;
            SendAttempts = 1;
            Message = message;
            Callback = callback;
        }
    }
}
