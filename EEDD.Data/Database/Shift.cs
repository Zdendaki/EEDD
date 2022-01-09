namespace ServerData.Database
{
    [Table("Shifts")]
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual Client Client { get; set; }

        [Precision(0)]
        public DateTime? StartTime { get; set; }

        [Precision(0)]
        public DateTime? EndTime { get; set; }
    }
}
