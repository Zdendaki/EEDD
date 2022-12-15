using Communication.Data;
using Communication.Procedures;

namespace ISOR.Database
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

        [Required]
        [Precision(0)]
        public DateTime LastUpdate { get; set; }
        
        [Required]
        public bool CancelledA { get; set; } = false;

        [Required]
        public bool CancelledD { get; set; } = false;

        public char? RowChar { get; set; }

        public TrainType? TypeA { get; set; }

        public TrainType? TypeD { get; set; }

        [MaxLength(5)]
        public string? RouteA { get; set; }
        
        [MaxLength(5)]
        public string? RouteD { get; set; }

        public virtual List<RowItem> RowItems { get; set; } = new();

        public virtual List<RowDataDelay> DelaysA { get; set; } = new();

        public virtual List<RowDataDelay> DelaysD { get; set; } = new();

        public bool? ApprovalA { get; set; }

        public bool? ApprovalD { get; set; }

        [MaxLength(50)]
        public string? ExceptionsA { get; set; }

        [MaxLength(50)]
        public string? ExceptionsD { get; set; }

        public short? SentMessagesA { get; set; }

        public short? SentMessagesD { get; set; }
    }

    [Table("RowItems")]
    public class RowItem
    {
        [Key]
        public int Id { get; set; }

        public byte[] Data { get; set; }

        public RowDataType DataType { get; set; }

        public RowDataName Name { get; set; }

        public bool Edit { get; set; } = false;

        [Precision(0)]
        public DateTime Timestamp { get; set; }
    }

    [Table("RowDataDelays")]
    public class RowDataDelay
    {
        [Key]
        public int Id { get; set; }

        public DelayReason Reason { get; set; }

        public short Minutes { get; set; }

        public int? TrainNumber { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    }

    public struct RowDataAcception
    {
        public DateTime Data { get; set; }

        public DateTime Changed { get; set; }

        public AcceptionState State { get; set; }

        public DateTime Accepted { get; set; }
    }

    public struct RowDataTrack
    {
        public string Track { get; set; }

        public bool Occupied { get; set; }
    }

    public struct RowDataSignaller
    {
        public int SignallerId { get; set; }

        public DateTime? Time { get; set; }

        public SignallerState Type { get; set; }

        public string? Name { get; set; }
    }
}
