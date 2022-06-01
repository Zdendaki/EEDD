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

        public List<TrainData> Trains { get; set; }

        public UserData User { get; set; }

        public ClientData(int id, string name, List<RowData> previousShift, List<StationData> stations, List<TrainData> trains, UserData user)
        {
            Id = id;
            Name = name;
            PreviousShift = previousShift;
            Stations = stations;
            Trains = trains;
            User = user;
        }
    }

    public class StationData
    {
        public class Track
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public bool Platform { get; set; } = false;

            public Track(int id, string name, bool platform)
            {
                Id = id;
                Name = name;
                Platform = platform;
            }
        }

        public class Signaller
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Comment { get; set; }

            public Signaller(int id, string name, string comment)
            {
                Id = id;
                Name = name;
                Comment = comment;
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbr { get; set; }

        public float TimePenalty { get; set; }

        public StationColor Color { get; set; }

        public List<ConnectionData> Connections { get; set; }

        public List<Track> Tracks { get; set; }

        public List<Signaller> Signallers { get; set; }

        public StationData(int id, string name, string abbr, float penalty, StationColor color, List<ConnectionData> connections, List<Track> tracks, List<Signaller> signallers)
        {
            Id = id;
            Name = name;
            Abbr = abbr;
            TimePenalty = penalty;
            Color = color;
            Connections = connections;
            Tracks = tracks;
            Signallers = signallers;
        }
    }

    public class ConnectionData
    {
        public class Track
        {
            public int Id { get; set; }

            public byte Number { get; set; }

            public RouteInterlocking Interlocking { get; set; }

            public int MinimumInterval { get; set; }

            public Track(int id, byte number, RouteInterlocking interlocking, int minimumInterval)
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

        public ConnectionData(int id, int stationId, string abbr, List<Track> connectionTracks)
        {
            Id = id;
            StationId = stationId;
            Abbr = abbr;
            Tracks = connectionTracks;
        }
    }

    public class TrainData
    {

    }

    public class UserData
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public UserRole Role { get; set; }

        public UserData(int id, string username, string name, UserRole role)
        {
            Id = id;
            Username = username;
            Name = name;
            Role = role;
        }
    }
}
