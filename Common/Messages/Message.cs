using MessagePack;

namespace Common.Messages
{
    [MessagePackObject]
    [Union(0, typeof(ResponseMessage))]
    [Union(1, typeof(DataRequestMessage))]
    [Union(2, typeof(Login.LoginMessage))]
    [Union(3, typeof(Data.RoutesMessage))]
    [Union(4, typeof(Data.RouteDataMessage))]
    [Union(5, typeof(Data.TrainsMessage))]
    [Union(6, typeof(Train.TrainEventMessage))]
    [Union(7, typeof(Train.PodjRequest))]
    [Union(8, typeof(Train.PodjResponse))]
    [Union(9, typeof(Login.ClaimClientMessage))]
    [Union(10, typeof(Login.ReconnectMessage))]
    public abstract class Message
    {
        [Key(0)]
        public Guid ID { get; init; }

        protected Message()
        {
            ID = Guid.NewGuid();
        }
    }
}
