using Common.Data;
using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Messages.Data
{
    [MessagePackObject]
    public class RouteDataMessage : Message
    {
        [Key(1)]
        public required Route Route { get; init; }

        public RouteDataMessage() : base() { }

        [SetsRequiredMembers]
        public RouteDataMessage(Route route) : base()
        {
            Route = route;
        }
    }
}
