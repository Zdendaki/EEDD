using Common.Data;
using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Messages.Data
{
    [MessagePackObject]
    public class RoutesMessage : Message
    {
        [Key(1)]
        public required List<RouteBase> Routes { get; init; }

        public RoutesMessage() : base() { }

        [SetsRequiredMembers]
        public RoutesMessage(List<RouteBase> routes) : base()
        {
            Routes = routes;
        }

        [SetsRequiredMembers]
        public RoutesMessage(IEnumerable<Route> routes) : base()
        {
            Routes = routes.Select(route => route.GetBase()).ToList();
        }
    }
}
