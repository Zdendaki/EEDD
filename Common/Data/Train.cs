using MessagePack;

#pragma warning disable MsgPack017

namespace Common.Data
{
    [MessagePackObject]
    public class Train
    {
        [Key(0)]
        public required uint ID { get; init; }

        [Key(1)]
        public required int Number { get; init; }

        [Key(2)]
        public DateTime Date { get; init; }

        [Key(3)]
        public List<TrainStop> Stops { get; init; }

        [Key(4)]
        public List<TrainEvent> Events { get; init; } = [];

        [Key(5)]
        public List<TrainComposition> Compositions { get; init; } = [];

        [Key(6)]
        public List<TrainReady> Readys { get; init; } = [];
    }

    [MessagePackObject]
    public class TrainStop
    {
        [Key(0)]
        public required uint ID { get; init; }

        [Key(1)]
        public required TrainType TypeArrival { get; init; }

        [Key(2)]
        public required TrainType TypeDeparture { get; init; }

        [Key(3)]
        public DateTime? Arrival { get; init; }

        [Key(4)]
        public DateTime? Departure { get; init; }

        [Key(5)]
        public string? TrackArrival { get; init; }

        [Key(6)]
        public string? TrackDeparture { get; init; }

        [Key(7)]
        public string? RouteTrackArrival { get; init; }

        [Key(8)]
        public required bool StartBetweenStations { get; init; }

        [Key(9)]
        public string? RouteTrackDeparture { get; init; }

        [Key(10)]
        public required bool EndBetweenStations { get; init; }

        [Key(11)]
        public List<string> Actions { get; init; }
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
        public required Guid ID { get; init; }

        [Key(1)]
        public required Guid Station { get; init; }

        [Key(2)]
        public required ushort MaxSpeed { get; init; }

        [Key(3)]
        public required ushort Length { get; init; }

        [Key(4)]
        public required ushort Axles { get; init; }

        [Key(5)]
        public ushort? Weight { get; init; }

        [Key(6)]
        public List<Vehicle> Vehicles { get; init; } = [];
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
        public required Guid ID { get; init; }

        [Key(1)]
        public required Guid Station { get; init; }

        [Key(2)]
        public required string Contact { get; init; }

        [Key(3)]
        public required DateTime Time { get; init; }
    }
}