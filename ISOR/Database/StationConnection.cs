using Communication.Data;

namespace ISOR.Database
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

        [Required]
        public float TravelTime { get; set; }

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

        [Required]
        public RouteInterlocking Interlocking { get; set; }

        public int MinimumInterval { get; set; } = 0;

        [Required]
        public DefaultDirection Direction { get; set; } = DefaultDirection.None;
    }
}
