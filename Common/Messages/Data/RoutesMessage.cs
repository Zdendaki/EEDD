using Common.Data;
using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Messages.Data
{
    [MessagePackObject]
    public class RoutesMessage : Message
    {
        [Key(1)]
        public List<RouteBase> Routes { get; init; }

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

        public RoutesMessage() : base() { }
    }
}
