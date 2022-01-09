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

        public virtual Client Client { get; set; }

        public virtual List<Row> Archive { get; set; } = new List<Row>();
    }

    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(6)]
        [Required]
        public string Name { get; set; }

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

        public virtual Station Station { get; set; }
    }
}
