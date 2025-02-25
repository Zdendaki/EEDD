using Common.Data;
using Common.XML;
using System.Xml;

namespace Server
{
    internal class RuntimeData
    {
        private readonly ILogger<RuntimeData> _logger;
        private readonly IConfiguration _config;

        public List<Route> Routes { get; }

        public RuntimeData(ILogger<RuntimeData> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            Routes = [];
        }

        #region Loading
        public void LoadData()
        {
            try
            {
                _logger.LogInformation("Loading runtime data...");

                DirectoryInfo dataDir = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, _config["DataDirectory"]!));
                foreach (FileInfo file in dataDir.EnumerateFiles("*.xml"))
                {
                    LoadRoute(file.FullName);
                }

                _logger.LogInformation("Runtime data loaded.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load runtime data.");
            }
        }

        private void LoadRoute(string path)
        {
            XmlReaderSettings settings = new()
            {
                IgnoreComments = true
            };

            using FileStream fs = new(path, FileMode.Open, FileAccess.Read);
            using XmlReader reader = XmlReader.Create(fs, settings);
            XmlDocument doc = new();
            doc.Load(reader);

            XmlNode? root = doc.DocumentElement;
            if (root is null)
                return;
            if (root.Name != "Route")
                return;
            if (root.ChildNodes.Count == 0)
                return;

            Guid id = root.GetGuidAttribute("ID");
            string name = root.GetStringAttribute("Name");
            string passwordEDD = root.GetStringAttribute("PasswordEDD");
            string passwordEVAL = root.GetStringAttribute("PasswordEVAL");
            List<Station> stations = [];
            List<StationConnection> connections = [];
            List<Client> clients = [];
            List<Train> trains = [];

            foreach (XmlNode child in root.ChildNodes)
            {
                switch (child.Name)
                {
                    case "Stations":
                        foreach (XmlNode station in child.ChildNodes)
                            stations.Add(LoadStation(station));
                        break;
                    case "StationConnections":
                        foreach (XmlNode stationConnection in child.ChildNodes)
                            connections.Add(LoadStationConnection(stationConnection));
                        break;
                    case "Clients":
                        foreach (XmlNode client in child.ChildNodes)
                            clients.Add(LoadClient(client));
                        break;
                    case "Trains":
                        foreach (XmlNode train in child.ChildNodes)
                            trains.Add(LoadTrain(train));
                        break;
                }
            }

            Routes.Add(new()
            {
                ID = id,
                Name = name,
                PasswordEDD = passwordEDD,
                PasswordEVAL = passwordEVAL,
                Stations = stations,
                StationConnections = connections,
                Clients = clients,
                Trains = trains
            });
        }

        private Station LoadStation(XmlNode node)
        {
            uint id = node.GetUInt32Attribute("ID");
            string name = node.GetStringAttribute("Name");
            string abbr = node.GetStringAttribute("Abbr");
            List<Track> tracks = [];

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "Tracks")
                {
                    foreach (XmlNode track in child.ChildNodes)
                        tracks.Add(LoadStationTrack(track));
                }
            }

            return new()
            {
                ID = id,
                Name = name,
                Abbr = abbr,
                Tracks = tracks
            };
        }

        private Track LoadStationTrack(XmlNode node)
        {
            uint id = node.GetUInt32Attribute("ID");
            string name = node.GetStringAttribute("Name");
            bool platform = node.GetBoolAttribute("Platform");

            return new()
            {
                ID = id,
                Name = name,
                Platform = platform
            };
        }

        private StationConnection LoadStationConnection(XmlNode node)
        {
            uint id = node.GetUInt32Attribute("ID");
            uint station1 = node.GetUInt32Attribute("Station1");
            uint station2 = node.GetUInt32Attribute("Station2");
            float travelTime = node.GetSingleAttribute("TravelTime");
            List<RouteTrack> tracks = [];

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "Tracks")
                {
                    foreach (XmlNode track in child.ChildNodes)
                        tracks.Add(LoadRouteTrack(track));
                }
            }

            return new()
            {
                ID = id,
                Station1 = station1,
                Station2 = station2,
                TravelTime = travelTime,
                Tracks = tracks
            };
        }

        private RouteTrack LoadRouteTrack(XmlNode node)
        {
            uint id = node.GetUInt32Attribute("ID");
            string name = node.GetStringAttribute("Name");
            ConnectionFlags flags = (ConnectionFlags)node.GetByteAttribute("Flags");

            return new()
            {
                ID = id,
                Name = name,
                Flags = flags
            };
        }

        private Client LoadClient(XmlNode node)
        {
            uint id = node.GetUInt32Attribute("ID");
            string name = node.GetStringAttribute("Name");
            List<ClientStation> stations = [];
            List<Signaller> signallers = [];

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "Stations")
                {
                    foreach (XmlNode station in child.ChildNodes)
                        stations.Add(LoadClientStation(station));
                }
                else if (child.Name == "Signallers")
                {
                    foreach (XmlNode signaller in child.ChildNodes)
                        signallers.Add(LoadSignaller(signaller));
                }
            }

            return new()
            {
                ID = id,
                Name = name,
                Stations = stations,
                Signallers = signallers
            };
        }

        private Signaller LoadSignaller(XmlNode node)
        {
            uint id = node.GetUInt32Attribute("ID");
            string name = node.GetStringAttribute("Name");
            SignallerType type = (SignallerType)node.GetByteAttribute("Type");
            string? comment = node.GetStringAttribute("Comment");

            return new()
            {
                ID = id,
                Name = name,
                Type = type,
                Comment = comment
            };
        }

        private ClientStation LoadClientStation(XmlNode child)
        {
            uint id = child.GetUInt32Attribute("ID");
            uint stationId = child.GetUInt32Attribute("StationID");
            StationColor color = child.GetEnumAttribute<StationColor>("Color");

            return new()
            {
                ID = id,
                StationID = stationId,
                Color = color
            };
        }

        private Train LoadTrain(XmlNode node)
        {
            Guid id = node.GetGuidAttribute("ID");
            string number = node.GetStringAttribute("Number");
            DateTime date = node.GetDateTimeAttribute("Date").Date;
            List<TrainStop> stops = [];

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "Stops")
                {
                    foreach (XmlNode stop in child.ChildNodes)
                        stops.Add(LoadTrainStop(stop));
                }
            }

            return new()
            {
                ID = id,
                Number = number,
                Date = date,
                Stops = stops
            };
        }

        private TrainStop LoadTrainStop(XmlNode stop)
        {
            uint id = stop.GetUInt32Attribute("ID");
            TrainType type = (TrainType)stop.GetByteAttribute("Type");
            int number = stop.GetInt32Attribute("Number");
            DateTime? arrival = stop.GetDateTimeAttributeN("Arrival");
            DateTime? departure = stop.GetDateTimeAttributeN("Departure");
            string? trackArrival = stop.GetStringAttribute("TrackArrival");
            string? trackDeparture = stop.GetStringAttribute("TrackDeparture");
            string? routeTrackArrival = stop.GetStringAttribute("RouteTrackArrival");
            bool startBetweenStations = stop.GetBoolAttribute("StartBetweenStations");
            string? routeTrackDeparture = stop.GetStringAttribute("RouteTrackDeparture");
            bool endBetweenStations = stop.GetBoolAttribute("EndBetweenStations");
            List<string> actions = [];

            foreach (XmlNode child in stop.ChildNodes)
            {
                if (child.Name == "Action" && !string.IsNullOrWhiteSpace(child.Value))
                    actions.Add(child.Value);
            }

            return new()
            {
                ID = id,
                Type = type,
                Number = number,
                Arrival = arrival,
                Departure = departure,
                TrackArrival = trackArrival,
                TrackDeparture = trackDeparture,
                RouteTrackArrival = routeTrackArrival,
                StartBetweenStations = startBetweenStations,
                RouteTrackDeparture = routeTrackDeparture,
                EndBetweenStations = endBetweenStations,
                Actions = actions
            };
        }
        #endregion
    }
}
