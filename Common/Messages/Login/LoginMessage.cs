using MessagePack;

namespace Common.Messages.Login
{
    [MessagePackObject]
    public class LoginMessage : Message
    {
        [Key(1)]
        public Guid RouteID { get; init; }

        [Key(2)]
        public string Username { get; init; }

        [Key(3)]
        public string Password { get; init; }
    }
}
