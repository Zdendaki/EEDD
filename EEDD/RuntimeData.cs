using Communication.Data;
using Communication.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEDD
{
    internal class RuntimeData
    {
        public int ShiftId { get; set; }

        public ClientData Client { get; set; }

        public List<IGrouping<(string name, SignallerType type), StationData.Signaller>> Signallers { get; set; }
        
        public RuntimeData()
        {

        }
    }
}
