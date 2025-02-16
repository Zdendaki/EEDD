using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Messages.Response
{
    [MessagePackObject]
    public class StringResponseMessage : ResponseMessage
    {
        [Key(3)]
        public required string Message { get; init; }

        public StringResponseMessage() { }

        [SetsRequiredMembers]
        public StringResponseMessage(Guid requestId, ResponseStatus status, string message) : base(requestId, status)
        {
            Message = message;
        }

        public static StringResponseMessage GetRefusedMessage(Guid requestID, string message)
        {
            return new(requestID, ResponseStatus.Refused, message);
        }
    }
}
