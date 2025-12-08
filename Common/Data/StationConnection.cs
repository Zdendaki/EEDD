using MessagePack;

namespace Common.Data
{
    [MessagePackObject]
    public class StationConnection
    {
        [Key(0)]
        public required uint ID { get; init; }

        [Key(1)]
        public required uint Station1 { get; init; }

        [Key(2)]
        public required uint Station2 { get; init; }

        [Key(3)]
        public required float TravelTime { get; init; }

        [Key(4)]
        public List<RouteTrack> Tracks { get; init; }
    }

    [MessagePackObject]
    public class RouteTrack
    {
        [Key(0)]
        public required uint ID { get; init; }

        [Key(1)]
        public required string Name { get; init; }

        [Key(2)]
        public required ConnectionFlags Flags { get; init; }
    }
}
