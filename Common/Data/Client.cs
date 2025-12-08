using MessagePack;

namespace Common.Data
{
    [MessagePackObject]
    public class Client
    {
        public const int MAX_SIGNALLERS = 6;

        [Key(0)]
        public required uint ID { get; init; }

        [Key(1)]
        public required string Name { get; init; }

        [Key(3)]
        public List<ClientStation> Stations { get; init; }

        [Key(4)]
        public List<Signaller> Signallers { get; init; }

        [Key(5)]
        public User? User { get; set; }

        public override string ToString()
        {
            if (User is null)
                return Name;
            else
                return $"{Name} ({User.Name})";
        }
    }

    [MessagePackObject]
    public class ClientStation
    {
        [Key(0)]
        public uint ID { get; init; }

        [Key(1)]
        public uint StationID { get; init; }

        [Key(2)]
        public StationColor Color { get; init; }
    }

    [MessagePackObject]
    public class Signaller
    {
        [Key(0)]
        public required uint ID { get; init; }

        [Key(1)]
        public required string Name { get; init; }

        [Key(2)]
        public required SignallerType Type { get; init; }

        [Key(3)]
        public string? Comment { get; init; }
    }
}
