using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Messages
{
    // Union 0
    [MessagePackObject]
    public class ResponseMessage : Message
    {
        [Key(1)]
        public required Guid RequestID { get; init; }

        [Key(2)]
        public required ResponseStatus Status { get; init; }

        public ResponseMessage() { }

        [SetsRequiredMembers]
        protected ResponseMessage(Guid requestID, ResponseStatus status)
        {
            RequestID = requestID;
            Status = status;
        }

        public static ResponseMessage GetAcceptedMessage(Guid requestID)
        {
            return new(requestID, ResponseStatus.Accepted);
        }

        public static ResponseMessage GetUnauthorizedMessage(Guid requestID)
        {
            return new(requestID, ResponseStatus.Unauthorized);
        }

        public static ResponseMessage GetBadCredentialsMessage(Guid requestID)
        {
            return new(requestID, ResponseStatus.BadCredentials);
        }

        public static ResponseMessage GetRefusedMessage(Guid requestID)
        {
            return new(requestID, ResponseStatus.Refused);
        }
    }

    public enum ResponseStatus : byte
    {
        Accepted,
        Refused,
        Unauthorized,
        BadCredentials
    }
}
