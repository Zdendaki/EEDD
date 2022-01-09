namespace ServerData.Database
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Route Route { get; set; }

        public virtual List<Station> Stations { get; set; } = new List<Station>();

        public virtual List<Shift> Shifts { get; set; } = new List<Shift>();

    }
}
