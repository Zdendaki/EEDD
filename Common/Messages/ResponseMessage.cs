using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Messages
{
    [MessagePackObject]
    public class ResponseMessage : Message
    {
        [Key(1)]
        public required Guid RequestID { get; init; }

        [Key(2)]
        public required ResponseStatus Status { get; init; }

        [Key(3)]
        public string? Message { get; init; }

        public ResponseMessage() { }

        [SetsRequiredMembers]
        public ResponseMessage(Guid requestID, ResponseStatus status, string? message = null)
        {
            RequestID = requestID;
            Status = status;
            Message = message;
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

        public static ResponseMessage GetRefusedMessage(Guid requestID, string? message = null)
        {
            return new(requestID, ResponseStatus.Refused, message);
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
