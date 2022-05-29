using Communication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Procedures
{
    public class UserRoute
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Primary { get; set; }

        public UserRoute(int id, string name, bool primary)
        {
            Id = id;
            Name = name;
            Primary = primary;
        }

        public override string ToString() => Name;
    }

    public class ClientInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Free { get; set; }

        public ClientInfo(int id, string name, bool free)
        {
            Id = id;
            Name = name;
            Free = free;
        }

        public override string ToString() => Name;
    }

    public class ClientData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<RowData> PreviousShift { get; set; }

        public List<StationData> Stations { get; set; }

        public ClientData(int id, string name, List<RowData> previousShift, List<StationData> stations)
        {
            Id = id;
            Name = name;
            PreviousShift = previousShift;
            Stations = stations;
        }
    }

    public class StationData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbr { get; set; }

        public float TimePenalty { get; set; }

        public List<Connection> Connections { get; set; }

        public StationData(int id, string name, string abbr, float penalty, List<Connection> connections)
        {
            Id = id;
            Name = name;
            Abbr = abbr;
            TimePenalty = penalty;
            Connections = connections;
        }
    }

    public class Connection
    {
        public class Track
        {
            public int Id { get; set; }

            public int Number { get; set; }

            public RouteInterlocking Interlocking { get; set; }

            public int MinimumInterval { get; set; }

            public Track(int id, int number, RouteInterlocking interlocking, int minimumInterval)
            {
                Id = id;
                Number = number;
                Interlocking = interlocking;
                MinimumInterval = minimumInterval;
            }
        }

        public int Id { get; set; }

        public int StationId { get; set; }

        public string Abbr { get; set; }

        public List<Track> Tracks { get; set; } = new();

        public Connection(int id, int stationId, string abbr, List<Track> connectionTracks)
        {
            Id = id;
            StationId = stationId;
            Abbr = abbr;
            Tracks = connectionTracks;
        }
    }


}
