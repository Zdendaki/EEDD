using Common.Data;
using MessagePack;

namespace Common.Messages.Data
{
    [MessagePackObject]
    public class RoutesMessage : Message
    {
        [Key(1)]
        public List<Route> Routes { get; init; }
    }
}
