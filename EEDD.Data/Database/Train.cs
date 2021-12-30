namespace ServerData.Database
{
    [Table("Trains")]
    [Index(nameof(Number))]
    public class Train
    {
        [Key]
        public int Id { get; set; }

        public TrainType Type { get; set; }

        public int Number { get; set; }

        public List<Stop> Stops { get; set; } = new List<Stop>();

        public Route Route { get; set; }
    }

    [Table("Stops")]
    public class Stop
    {
        [Key]
        public int Id { get; set; }

        [Precision(0)]
        public DateTime? Arrival { get; set; }

        [Precision(0)]
        public DateTime? Departure { get; set; }

        [Required]
        public Track StationTrack { get; set; }

        [Required]
        public int StationTrackId { get; set; }

        public Train Train { get; set; }

        public RouteTrack? From { get; set; }

        public RouteTrack? To { get; set; }

        public int? FromId { get; set; }

        public int? ToId { get; set; }
    }
}
