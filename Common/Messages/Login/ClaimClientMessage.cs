using MessagePack;
using System.Diagnostics.CodeAnalysis;

namespace Common.Messages.Login
{
    // Union 9
    [MessagePackObject]
    public class ClaimClientMessage : Message
    {
        [Key(1)]
        public required uint ClientID { get; init; }

        public ClaimClientMessage() { }

        [SetsRequiredMembers]
        public ClaimClientMessage(uint stationID)
        {
            ClientID = stationID;
        }
    }
}
