using Common.Data;
using MessagePack;

namespace Common.Messages.Train
{
    [MessagePackObject]
    public class TrainEventMessage : Message
    {
        [Key(1)]
        public uint TrainID { get; init; }

        [Key(2)]
        public EventType Type { get; init; }

        [Key(3)]
        public DateTime Time { get; init; }

        [Key(4)]
        public uint Station { get; init; }

        [Key(5)]
        public string StationTrack { get; init; }

        [Key(6)]
        public string RouteTrack { get; init; }

        [Key(7)]
        public string? StationTrackD { get; init; } // průjezd

        [Key(8)]
        public string? RouteTrackD { get; init; } // průjezd
    }
}
