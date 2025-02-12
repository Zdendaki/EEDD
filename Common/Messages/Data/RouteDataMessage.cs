using Common.Data;
using MessagePack;

namespace Common.Messages.Data
{
    [MessagePackObject]
    public class RouteDataMessage : Message
    {
        [Key(1)]
        public Route Route { get; init; }

        public RouteDataMessage() : base() { }

        public RouteDataMessage(Route route) : base()
        {
            Route = route;
        }
    }
}
