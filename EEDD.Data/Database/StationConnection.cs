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

        public virtual Station Primary { get; set; }

        public virtual Station Secondary { get; set; }

        public virtual List<RouteTrack> RouteTracks { get; set; } = new List<RouteTrack>();

        public virtual Route Route { get; set; }

        public int PrimaryId { get; set; }

        public int SecondaryId { get; set; }
    }

    [Table("RouteTracks")]
    public class RouteTrack
    {
        [Key]
        public int Id { get; set; }

        public byte Number { get; set; }

        public virtual StationConnection Connection { get; set; }
        
        public bool Interlocking { get; set; } = true;

        public int MinimumInterval { get; set; } = 0;
    }
}
