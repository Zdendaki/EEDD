using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEDD.Data.Database
{
    [Table("Stations")]
    public class Station
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public string Abbr { get; set; }

        public List<Track> Tracks { get; set; } = new List<Track>();

        public List<Signaller> Signallers { get; set; } = new List<Signaller>();

        public Route Route { get; set; }
    }

    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(6)]
        [Required]
        public string Name { get; set; }

        public Station Station { get; set; }
    }

    [Table("Signallers")]
    public class Signaller
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(2)]
        [Required]
        public string Name { get; set; }

        [MaxLength(64)]
        [Required]
        public string Comment { get; set; }

        public Station Station { get; set; }
    }
}
