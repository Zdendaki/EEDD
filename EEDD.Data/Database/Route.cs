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

        public List<Client> Clients { get; set; } = new();

        public List<StationConnection> Connections { get; set; } = new();

        public List<Train> Trains { get; set; } = new();

        public List<User> Users { get; set; } = new();

        public List<Timetable> Timetables { get; set; } = new();
    }
}
