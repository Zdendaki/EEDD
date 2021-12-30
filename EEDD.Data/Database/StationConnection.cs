namespace ServerData.Database
{
    [Table("StationConnections")]
    public class StationConnection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public Station Primary { get; set; }

        public Station Secondary { get; set; }

        public bool Interlocking { get; set; } = true;

        public List<RouteTrack> RouteTracks { get; set; } = new List<RouteTrack>();

        public Route Route { get; set; }

        public int PrimaryId { get; set; }

        public int SecondaryId { get; set; }
    }

    [Table("RouteTracks")]
    public class RouteTrack
    {
        [Key]
        public int Id { get; set; }

        public byte Number { get; set; }

        public StationConnection Connection { get; set; }
        
        public bool Secured { get; set; } = true;

        public int MinimalDelay { get; set; } = 0;
    }
}
