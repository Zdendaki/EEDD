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

        public virtual List<Client> Clients { get; set; } = new();

        public virtual List<StationConnection> Connections { get; set; } = new();

        public virtual List<Train> Trains { get; set; } = new();

        public virtual List<User> Users { get; set; } = new();

        public virtual List<Timetable> Timetables { get; set; } = new();
    }
}
