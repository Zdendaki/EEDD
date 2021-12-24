using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEDD.Data.Database
{
    [Table("StationConnections")]
    public class StationConnection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public Station Primary { get; set; }

        public Station Secondary { get; set; }

        public bool Interlocking { get; set; } = true;

        public List<RouteTrack> RouteTracks { get; set; } = new List<RouteTrack>();

        public Route Route { get; set; }
    }

    [Table("RouteTracks")]
    public class RouteTrack
    {
        [Key]
        public int Id { get; set; }

        public byte Number { get; set; }

        public StationConnection Connection { get; set; }
    }
}
