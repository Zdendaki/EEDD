using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEDD.Data.Database
{
    [Table("ArchiveRecords")]
    public class Archive
    {
        [Key]
        public int Id { get; set; }

        public RowType RowType { get; set; }

        [MaxLength(256)]
        public string? Left { get; set; }

        [Precision(0)]
        public DateTime? LeftTime { get; set; }

        [MaxLength(256)]
        public string? Right { get; set; }

        [Precision(0)]
        public DateTime? RightTime { get; set; }


    }

    public enum RowType
    {
        Arrival,
        Departure,
        Both,
        ShortBlue,
        ShortRed,
        LongRed,
        LongBlue
    }
}
