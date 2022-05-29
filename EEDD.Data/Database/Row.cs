using Communication.Data;

namespace ServerData.Database
{
    [Table("Rows")]
    public class Row
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public RowType RowType { get; set; }

        [Required]
        public virtual User ResponsibleUser { get; set; }

        [Required]
        public virtual Station Station { get; set; }

        [Required]
        public bool RowComplete { get; set; } = false;

        public virtual Train? Train { get; set; }

        [Precision(0)]
        public DateTime LastUpdate { get; set; }
        
        [Required]
        public bool CancelledA { get; set; } = false;

        [Required]
        public bool CancelledD { get; set; } = false;

        public char? RowChar { get; set; }

        public virtual RowDataString? Caption { get; set; }

        public virtual RowDataString? Message { get; set; }

        public int? NumberA { get; set; }

        public int? NumberD { get; set; }

        public TrainType? TypeA { get; set; }

        public TrainType? TypeD { get; set; }

        [MaxLength(5)]
        public string? RouteA { get; set; }
        
        [MaxLength(5)]
        public string? RouteD { get; set; }

        public virtual RowDataDate? AnnouncedA { get; set; }

        public virtual RowDataAcception? AnnouncedD { get; set; }

        public virtual RowDataDate? AcceptedA { get; set; }

        public virtual RowDataDate? AcceptedD { get; set; }

        public virtual RowDataDate? APMD { get; set; }

        public virtual RowDataDate? DPMD { get; set; }

        public virtual RowDataDate? ActualDepA { get; set; }

        public virtual RowDataDate? ActualDepD { get; set; }

        public virtual RowDataTrack? TrackA { get; set; }

        public virtual RowDataTrack? TrackD { get; set; }

        public virtual RowDataDate? Sig1A { get; set; }

        public virtual RowDataDate? Sig2A { get; set; }

        public virtual RowDataDate? Sig3A { get; set; }

        public virtual RowDataDate? Sig4A { get; set; }

        public virtual RowDataDate? Sig1D { get; set; }

        public virtual RowDataDate? Sig2D { get; set; }

        public virtual RowDataDate? Sig3D { get; set; }

        public virtual RowDataDate? Sig4D { get; set; }

        public virtual RowDataDate? Arrival { get; set; }

        public virtual RowDataDate? Departure { get; set; }

        public short? ADelay { get; set; }

        public short? DDelay { get; set; }

        public virtual List<RowDataDelay> Delays { get; set; } = new();

        public virtual RowDataDate? DepartedA { get; set; }

        public virtual RowDataDate? DepartedD { get; set; }

        public virtual RowDataString? NoteA { get; set; }

        public virtual RowDataString? NoteD { get; set; }

        public bool? ApprovalA { get; set; }

        public bool? ApprovalD { get; set; }

        [MaxLength(256)]
        public string? ExceptionsA { get; set; }

        [MaxLength(256)]
        public string? ExceptionsD { get; set; }

        public short? SentMessagesA { get; set; }

        public short? SentMessagesD { get; set; }

        public virtual RowDataString? CommentA { get; set; }

        public virtual RowDataString? CommentD { get; set; }
    }

    [Table("RowDataStrings")]
    public class RowDataString
    {
        [Key]
        public int Id { get; set; }

        public string Data { get; set; }

        [Precision(0)]
        public DateTime Changed { get; set; }

        public static implicit operator (string, DateTime)?(RowDataString? data) => data is not null ? (data.Data, data.Changed) : null;
    }

    [Table("RowDataDates")]
    public class RowDataDate
    {
        [Key]
        public int Id { get; set; }

        [Precision(0)]
        public DateTime Data { get; set; }

        [Precision(0)]
        public DateTime Changed { get; set; }

        public static implicit operator (DateTime, DateTime)?(RowDataDate? data) => data is not null ? (data.Data, data.Changed) : null;
    }

    [Table("RowDataAcceptions")]
    public class RowDataAcception
    {
        [Key]
        public int Id { get; set; }

        [Precision(0)]
        public DateTime Data { get; set; }

        [Precision(0)]
        public DateTime Changed { get; set; }

        public AcceptionState State { get; set; }

        [Precision(0)]
        public DateTime Accepted { get; set; }
    }

    [Table("RowDataTracks")]
    public class RowDataTrack
    {
        [Key]
        public int Id { get; set; }

        public string Track { get; set; }

        public bool Occupied { get; set; }

        [Precision(0)]
        public DateTime Changed { get; set; }
    }

    [Table("RowDataDelays")]
    public class RowDataDelay
    {
        [Key]
        public int Id { get; set; }

        public DelayReason Reason { get; set; }

        public short Minutes { get; set; }

        public int? TrainNumber { get; set; }
    }
}
