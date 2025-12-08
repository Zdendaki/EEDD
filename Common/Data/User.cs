using MessagePack;

namespace Common.Data
{
    [MessagePackObject]
    public class User
    {
        [Key(0)]
        public Guid ID { get; init; }

        [Key(1)]
        public uint DeviceID { get; init; }

        [Key(2)]
        public string Name { get; init; }

        public User() { }

        public User(uint deviceId, string name)
        {
            ID = Guid.NewGuid();
            DeviceID = deviceId;
            Name = name;
        }

        public static bool operator ==(User? a, User? b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;

            return a.ID == b.ID;
        }

        public static bool operator !=(User? a, User? b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            if (obj is User user)
                return ID == user.ID;

            return false;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
