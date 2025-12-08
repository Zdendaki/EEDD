using MessagePack;

namespace Common.Data
{
    [MessagePackObject]
    public class Station
    {
        [Key(0)]
        public required uint ID { get; init; }

        [Key(1)]
        public required string Name { get; init; }

        [Key(2)]
        public required string Abbr { get; init; }

        [Key(3)]
        public List<Track> Tracks { get; init; }
    }

    [MessagePackObject]
    public class Track
    {
        [Key(0)]
        public required uint ID { get; init; }

        [Key(1)]
        public string Name { get; init; }

        [Key(2)]
        public bool Platform { get; init; }
    }
}
