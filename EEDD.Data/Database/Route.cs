namespace ServerData.Database
{
    [Table("Routes")]
    public class Route
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public List<Client> Clients { get; set; } = new List<Client>();

        public List<StationConnection> Connections { get; set; } = new List<StationConnection>();

        public List<Train> Trains { get; set; } = new List<Train>();

        public List<User> Users { get; set; } = new List<User>();
    }
}
