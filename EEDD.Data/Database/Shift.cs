using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData.Database
{
    [Table("Shifts")]
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public Client Client { get; set; }

        [Precision(0)]
        public DateTime? StartTime { get; set; }

        [Precision(0)]
        public DateTime? EndTime { get; set; }
    }
}
