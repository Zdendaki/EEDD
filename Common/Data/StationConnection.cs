using MessagePack;
using System.Xml.Serialization;

namespace Common.Data
{
    [MessagePackObject, XmlRoot]
    public class StationConnection
    {
        [Key(0), XmlAttribute]
        public required uint ID { get; init; }

        [Key(1), XmlAttribute]
        public required uint Station1 { get; init; }

        [Key(2), XmlAttribute]
        public required uint Station2 { get; init; }

        [Key(3), XmlAttribute]
        public required float TravelTime { get; init; }

        [Key(4)]
        public List<RouteTrack> Tracks { get; init; }
    }

    [MessagePackObject, XmlRoot]
    public class RouteTrack
    {
        [Key(0), XmlAttribute]
        public required uint ID { get; init; }

        [Key(1), XmlAttribute]
        public required string Name { get; init; }

        [Key(2), XmlAttribute]
        public required ConnectionFlags Flags { get; init; }
    }
}
