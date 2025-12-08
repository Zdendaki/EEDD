using Common.Data;

namespace EEDD.Controls
{
    public class TrainRowData : RowData
    {
        public Guid ID { get; set; }

        public DateTime Timestamp { get; set; }

        public RowType Type { get; set; }

        public int TrainNuber { get; set; }

        public TrainType Category { get; set; }

        public string Direction { get; set; }

        public string? DirectionTrack { get; set; }

        public DateTime? Announced { get; set; }

        public AnnounceState AnnounceState { get; set; }

        public DateTime? Accepted { get; set; }

        public DateTime? Departed { get; set; }

        public Track? Track { get; set; }

        public DateTime? Time { get; set; }

        public short? Delay { get; set; }

        public string? Comment { get; set; }
    }
}
