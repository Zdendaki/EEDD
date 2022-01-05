using Communication.Procedures.Trains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public class RowData
    {
        public int ArchiveId { get; set; }

        public bool CanModify { get; set; }
        
        public RowValue<int> TrainNumber { get; set; }

        public RowValue<TrainType> TrainType { get; set; }

        public RowValue<string> Stations { get; set; }

        public RowValue<int> TrackNumbers { get; set; }

        public RowValue<DateTime> Announcements { get; set; }

        
    }

    public class RowValue<T>
    {
        public T? AValue { get; set; }

        public DateTime? AChanged { get; set; }

        public T? DValue { get; set; }

        public DateTime? DChanged { get; set; }

        public RowValue(T? Avalue, DateTime? Achanged, T? Dvalue, DateTime? Dchanged)
        {
            AValue = Avalue;
            AChanged = Achanged;
            DValue = Dvalue;
            DChanged = Dchanged;
        }
    }

    public class SignallerData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }
    }
}
