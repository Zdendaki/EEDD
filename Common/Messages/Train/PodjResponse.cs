using MessagePack;

namespace Common.Messages.Train
{
    // Union 8
    [MessagePackObject]
    public class PodjResponse : Message
    {
        [Key(1)]
        public Guid RequestID { get; init; }

        [Key(2)]
        public uint TrainID { get; init; }

        [Key(3)]
        public uint StationFrom { get; init; }

        [Key(4)]
        public uint StationTo { get; init; }

        [Key(5)]
        public bool Accepted { get; init; }

        [Key(6)]
        public string? ErrorMessage { get; init; }
    }
}
