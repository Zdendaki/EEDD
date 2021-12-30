using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerData.Database
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public Route Route { get; set; }

        public bool Occupied { get; set; } = false;

        public List<Station> Stations { get; set; } = new List<Station>();

        public List<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
