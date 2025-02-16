using Common.Data;
using MessagePack;

namespace Common.Messages.Login
{
    // Union 2
    [MessagePackObject]
    public class LoginMessage : Message
    {
        [Key(1)]
        public required Guid RouteID { get; init; }

        [Key(2)]
        public required uint DeviceID { get; init; }

        [Key(3)]
        public required string Username { get; init; }

        [Key(4)]
        public required string Password { get; init; }

        public User GetUser(Guid id)
        {
            return new()
            {
                ID = id,
                DeviceID = DeviceID,
                Name = Username
            };
        }

        public User GetUser()
        {
            return new(DeviceID, Username);
        }
    }
}
