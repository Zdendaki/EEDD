namespace ServerData.Database
{
    [Table("Timetables")]
    public class Timetable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Route Route { get; set; }

        public List<TimetableTrain> Trains { get; set; } = new();
    }
}
