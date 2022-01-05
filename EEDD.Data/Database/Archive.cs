namespace ServerData.Database
{
    [Table("ArchiveRecords")]
    public class Archive
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public RowType RowType { get; set; }

        [Required]
        public User ResponsibleUser { get; set; }

        [Required]
        public Client Client { get; set; }

        [Required]
        public bool RowComplete { get; set; } = false;

        public Train? Train { get; set; }

        [MaxLength(256)]
        public string? Left { get; set; }

        [Precision(0)]
        public DateTime? LeftTime { get; set; }

        [MaxLength(256)]
        public string? Right { get; set; }

        [Precision(0)]
        public DateTime? RightTime { get; set; }

        public int? ANumber { get; set; }

        public int? DNumber { get; set; }

        public TrainType? AType { get; set; }

        public TrainType? DType { get; set; }

        public RouteTrack? ARoute { get; set; }

        public RouteTrack? DRoute { get; set; }

        [Precision(0)]
        public DateTime? AAnnounced { get; set; }

        [Precision(0)]
        public DateTime? DAnnounced { get; set; }

        [Precision(0)]
        public DateTime? AAnnouncedTime { get; set; }

        [Precision(0)]
        public DateTime? DAnnouncedTime { get; set; }

        [Precision(0)]
        public DateTime? APMD { get; set; }

        [Precision(0)]
        public DateTime? DPMD { get; set; }

        [Precision(0)]
        public DateTime? APMDTime { get; set; }

        [Precision(0)]
        public DateTime? DPMDTime { get; set; }

        [Precision(0)]
        public DateTime? AActualDep { get; set; }

        [Precision(0)]
        public DateTime? DActualDep { get; set; }

        [Precision(0)]
        public DateTime? AActualDepTime { get; set; }

        [Precision(0)]
        public DateTime? DActualDepTime { get; set; }

        public Track? ATrack { get; set; }

        public Track? DTrack { get; set; }

        [Precision(0)]
        public DateTime? ATrackTime { get; set; }

        [Precision(0)]
        public DateTime? DTrackTime { get; set; }

        [Precision(0)]
        public DateTime? Sig1A { get; set; }

        [Precision(0)]
        public DateTime? Sig2A { get; set; }

        [Precision(0)]
        public DateTime? Sig3A { get; set; }

        [Precision(0)]
        public DateTime? Sig4A { get; set; }

        [Precision(0)]
        public DateTime? Sig1D { get; set; }

        [Precision(0)]
        public DateTime? Sig2D { get; set; }

        [Precision(0)]
        public DateTime? Sig3D { get; set; }

        [Precision(0)]
        public DateTime? Sig4D { get; set; }

        [Precision(0)]
        public DateTime? Sig1ATime { get; set; }

        [Precision(0)]
        public DateTime? Sig2ATime { get; set; }

        [Precision(0)]
        public DateTime? Sig3ATime { get; set; }

        [Precision(0)]
        public DateTime? Sig4ATime { get; set; }

        [Precision(0)]
        public DateTime? Sig1DTime { get; set; }

        [Precision(0)]
        public DateTime? Sig2DTime { get; set; }

        [Precision(0)]
        public DateTime? Sig3DTime { get; set; }

        [Precision(0)]
        public DateTime? Sig4DTime { get; set; }

        [Precision(0)]
        public DateTime? Arrival { get; set; }

        [Precision(0)]
        public DateTime? Departure { get; set; }

        [Precision(0)]
        public DateTime? ArrivalTime { get; set; }

        [Precision(0)]
        public DateTime? DepartureTime { get; set; }

        public short? ADelay { get; set; }

        public short? DDelay { get; set; }

        [Precision(0)]
        public DateTime? ADeparted { get; set; }

        [Precision(0)]
        public DateTime? DDeparted { get; set; }

        [Precision(0)]
        public DateTime? ADepartedTime { get; set; }

        [Precision(0)]
        public DateTime? DDepartedTime { get; set; }

        [MaxLength(512)]
        public string? ANote { get; set; }

        [MaxLength(512)]
        public string? DNote { get; set; }

        [Precision(0)]
        public DateTime? ANoteTime { get; set; }

        [Precision(0)]
        public DateTime? DNoteTime { get; set; }

        public bool? AApproval { get; set; }

        public bool? DApproval { get; set; }

        [MaxLength(256)]
        public string? AExceptions { get; set; }

        [MaxLength(256)]
        public string? DExceptions { get; set; }

        public short? ASentMessages { get; set; }

        public short? DSentMessages { get; set; }

        [MaxLength(32)]
        public string? AComment { get; set; }

        [MaxLength(32)]
        public string? DComment { get; set; }

        [Precision(0)]
        public DateTime? ACommentTime { get; set; }

        [Precision(0)]
        public DateTime? DCommentTime { get; set; }
    }
}
