using Communication.Data;

namespace ServerData.Database
{
    [Table("Stations")]
    public class Station
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public string Abbr { get; set; }

        [Required]
        public float TimePenalty { get; set; } = 0f;

        [Required]
        public StationColor Color { get; set; } = StationColor.Gray;

        public virtual Client Client { get; set; }

        public virtual List<Row> Archive { get; set; } = new List<Row>();

        public virtual List<Track> Tracks { get; set; } = new List<Track>();

        public virtual List<Signaller> Signallers { get; set; } = new List<Signaller>();
    }

    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(6)]
        [Required]
        public string Name { get; set; }

        [Required]
        public bool Platform { get; set; } = false;

        public virtual Station Station { get; set; }

        public virtual List<Stop> TrainStops { get; set; } = new List<Stop>();
    }

    [Table("Signallers")]
    public class Signaller
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(2)]
        [Required]
        public string Name { get; set; }

        [MaxLength(64)]
        [Required]
        public string Comment { get; set; }

        public virtual List<Station> Stations { get; set; }
    }
}
