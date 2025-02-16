using Common.Data;
using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Messages.Login
{
    // Union 12
    [MessagePackObject]
    public class LoginResponseMessage : ResponseMessage
    {
        [Key(3)]
        public User User { get; init; }

        [Key(4)]
        public Guid Secret { get; init; }

        public LoginResponseMessage() { }

        [SetsRequiredMembers]
        public LoginResponseMessage(Guid requestID, User user, Guid secret) : base(requestID, ResponseStatus.Accepted)
        {
            User = user;
            Secret = secret;
        }
    }
}
