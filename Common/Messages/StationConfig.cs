using MessagePack;

namespace Common.Messages
{
    [MessagePackObject]
    public class StationConfig
    {
        [Key(0)]
        public required Guid ID { get; init; }

        [Key(1)]
        public required string Name { get; init; }

        [Key(2)]
        public required string Abbr { get; init; }
    }
}
