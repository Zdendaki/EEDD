using MessagePack;
using System.Xml.Serialization;

namespace Common.Data
{
    [MessagePackObject, XmlRoot]
    public class Client
    {
        [Key(0), XmlAttribute]
        public required uint ID { get; init; }

        [Key(1), XmlAttribute]
        public required string Name { get; init; }

        [Key(3)]
        public List<ClientStation> Stations { get; init; }

        [Key(4), XmlIgnore]
        public User? User { get; set; }
    }

    [MessagePackObject, XmlRoot]
    public class ClientStation
    {
        [Key(0), XmlAttribute]
        public uint ID { get; init; }

        [Key(1), XmlAttribute]
        public uint StationID { get; init; }

        [Key(2), XmlAttribute]
        public StationColor Color { get; init; }
    }
}
