using MessagePack;
using System.Xml.Serialization;

#pragma warning disable MsgPack017

namespace Common.Data
{
    [MessagePackObject, XmlRoot]
    public class Train
    {
        [Key(0), XmlAttribute]
        public required uint ID { get; init; }

        [Key(1), XmlAttribute]
        public required int Number { get; init; }

        [Key(2), XmlAttribute]
        public required DateTime Date { get; init; }

        [Key(3)]
        public List<TrainStop> Stops { get; init; }

        [Key(4), XmlIgnore]
        public List<TrainEvent> Events { get; init; } = [];

        [Key(5), XmlIgnore]
        public List<TrainComposition> Compositions { get; init; } = [];

        [Key(6), XmlIgnore]
        public List<TrainReady> Readys { get; init; } = [];
    }

    [MessagePackObject, XmlRoot]
    public class TrainStop
    {
        [Key(0), XmlAttribute]
        public required TrainType TypeArrival { get; init; }

        [Key(1), XmlAttribute]
        public required TrainType TypeDeparture { get; init; }

        [Key(2), XmlAttribute]
        public DateTime? Arrival { get; init; }

        [Key(3), XmlAttribute]
        public DateTime? Departure { get; init; }

        [Key(4), XmlAttribute]
        public required string TrackArrival { get; init; }

        [Key(5), XmlAttribute]
        public required string TrackDeparture { get; init; }

        [Key(6), XmlAttribute]
        public string? RouteTrackArrival { get; init; }

        [Key(7), XmlAttribute]
        public required bool StartBetweenStations { get; init; }

        [Key(8), XmlAttribute]
        public string? RouteTrackDeparture { get; init; }

        [Key(9), XmlAttribute]
        public required bool EndBetweenStations { get; init; }
    }

    [MessagePackObject]
    public class TrainEvent
    {
        [Key(1)]
        public required EventType Type { get; init; }

        [Key(2)]
        public required DateTime TimeSpan { get; init; }

        [Key(3)]
        public required DateTime EventTime { get; init; }

        [Key(4)]
        public required Guid Station { get; init; }

        [Key(5)]
        public required uint User { get; init; }
    }

    [MessagePackObject]
    public class TrainComposition
    {
        [Key(0)]
        public required Guid Station { get; init; }

        [Key(1)]
        public required ushort MaxSpeed { get; init; }

        [Key(2)]
        public required ushort Length { get; init; }

        [Key(3)]
        public required ushort Axles { get; init; }

        [Key(4)]
        public ushort? Weight { get; init; }

        [Key(5)]
        public List<Vehicle> Vehicles { get; init; }
    }

    [MessagePackObject]
    public class Vehicle
    {
        [Key(0)]
        public string Name { get; init; }

        [Key(1)]
        public ushort Length { get; init; }

        [Key(2)]
        public ushort Axles { get; init; }

        [Key(3)]
        public ushort? Weight { get; init; }
    }

    [MessagePackObject]
    public class TrainReady
    {
        [Key(0)]
        public required Guid Station { get; init; }

        [Key(1)]
        public required string Contact { get; init; }

        [Key(2)]
        public required DateTime Time { get; init; }
    }
}