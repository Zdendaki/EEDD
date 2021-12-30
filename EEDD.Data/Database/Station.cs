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

        public Client Client { get; set; }
    }

    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(6)]
        [Required]
        public string Name { get; set; }

        public Station Station { get; set; }

        public List<Stop> TrainStops { get; set; } = new List<Stop>();
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

        public Station Station { get; set; }
    }
}
