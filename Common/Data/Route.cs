using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Data
{
    [MessagePackObject]
    public class Route
    {
        [Key(0)]
        public required Guid ID { get; init; }

        [Key(1)]
        public required string Name { get; init; }

        [IgnoreMember]
        public string PasswordEDD { get; init; }

        [IgnoreMember]
        public string PasswordEVAL { get; init; }

        [Key(2)]
        public List<Station> Stations { get; init; }

        [Key(3)]
        public List<StationConnection> StationConnections { get; init; }

        [Key(4)]
        public List<Client> Clients { get; init; }

        [IgnoreMember]
        public List<Train> Trains { get; init; } = [];

        public RouteBase GetBase() => new(ID, Name);
    }

    [MessagePackObject]
    public class RouteBase
    {
        [Key(0)]
        public required Guid ID { get; init; }

        [Key(1)]
        public required string Name { get; init; }

        [SetsRequiredMembers]
        public RouteBase(Guid id, string name)
        {
            ID = id;
            Name = name;
        }

        public RouteBase() { }

        public override string ToString()
        {
            return Name;
        }
    }
}
