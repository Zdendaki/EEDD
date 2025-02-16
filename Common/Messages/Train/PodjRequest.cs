using MessagePack;

namespace Common.Messages.Train
{
    // Union 7
    [MessagePackObject]
    public class PodjRequest : Message
    {
        [Key(1)]
        public uint TrainID { get; init; }

        [Key(2)]
        public uint StationFrom { get; init; }

        [Key(3)]
        public uint StationTo { get; init; }

        [Key(4)]
        public DateTime Time { get; init; }

        [Key(5)]
        public string Track { get; init; }
    }
}
