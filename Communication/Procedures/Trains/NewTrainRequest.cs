using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Procedures.Trains
{
    public class NewTrainRequest : Procedure
    {
        public TrainType TrainType { get; set; }

        public int TrainNumber { get; set; }

        
    }

    public enum TrainType
    {
        Ex,
        R,
        Sp,
        Os,
        Sv,
        Nex,
        Pn,
        Mn,
        Lv,
        Sluz,
        PMD,
        ND
    }
}
