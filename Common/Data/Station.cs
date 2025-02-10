using MessagePack;
using System.Xml.Serialization;

namespace Common.Data
{
    [MessagePackObject, XmlRoot]
    public class Station
    {
        [Key(0), XmlAttribute]
        public required uint ID { get; init; }

        [Key(1), XmlAttribute]
        public required string Name { get; init; }

        [Key(2), XmlAttribute]
        public required string Abbr { get; init; }

        [Key(3)]
        public List<Track> Tracks { get; init; }

        [Key(4)]
        public List<Signaller> Signallers { get; init; }
    }

    [MessagePackObject, XmlRoot]
    public class Track
    {
        [Key(0), XmlAttribute]
        public required uint ID { get; init; }

        [Key(1), XmlAttribute]
        public string Name { get; init; }

        [Key(2), XmlAttribute]
        public bool Platform { get; init; }
    }

    [MessagePackObject, XmlRoot]
    public class Signaller
    {
        [Key(0), XmlAttribute]
        public required uint ID { get; init; }

        [Key(1), XmlAttribute]
        public required string Name { get; init; }

        [Key(2), XmlAttribute]
        public required SignallerType Type { get; init; }

        [Key(3), XmlAttribute]
        public string? Comment { get; init; }
    }
}
