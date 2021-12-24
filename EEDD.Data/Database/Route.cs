using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEDD.Data.Database
{
    [Table("Routes")]
    public class Route
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        public List<Station> Stations { get; set; } = new List<Station>();

        public List<StationConnection> Connections { get; set; } = new List<StationConnection>();

        public List<User> Users { get; set; } = new List<User>();
    }
}
