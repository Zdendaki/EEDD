using MessagePack;
using System.Xml.Serialization;

namespace Common.Data
{
    [MessagePackObject, XmlRoot]
    public class Route
    {
        [Key(0), XmlAttribute]
        public required Guid ID { get; init; }

        [Key(1), XmlAttribute]
        public required string Name { get; init; }

        [Key(2), XmlAttribute]
        public required string Password { get; init; }

        [Key(3)]
        public List<Station> Stations { get; init; }

        [Key(4)]
        public List<StationConnection> StationConnections { get; init; }

        [Key(5)]
        public List<Client> Clients { get; init; }

        [IgnoreMember]
        public List<Train> Trains { get; init; } = [];
    }
}
