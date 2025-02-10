using MessagePack;

namespace Common.Messages
{
    [MessagePackObject]
    public class ResponseMessage : Message
    {
        [Key(1)]
        public Guid RequestID { get; init; }

        [Key(2)]
        public ResponseStatus Status { get; init; }

        [Key(3)]
        public string? Message { get; init; }
    }

    public enum ResponseStatus : ushort
    {
        Accepted,
        Refused
    }
}
