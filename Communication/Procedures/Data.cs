using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Procedures
{
    public class UserRoute
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Primary { get; set; }

        public UserRoute(int id, string name, bool primary)
        {
            Id = id;
            Name = name;
            Primary = primary;
        }

        public override string ToString() => Name;
    }

    public class ClientInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Free { get; set; }

        public ClientInfo(int id, string name, bool free)
        {
            Id = id;
            Name = name;
            Free = free;
        }

        public override string ToString() => Name;
    }

    public class ClientData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbr { get; set; }

        public List<RowData> PreviousShift { get; set; }
    }

    public enum LoginState
    {
        Success,
        BadPassword,
        UserBanned,
        UnsufficentRights
    }

    public enum TokenState
    {
        Ok,
        Invalid,
        Expired,
        UnsufficentRights
    }

    public enum ResponseState
    {
        Success,
        InvalidToken,
        ExpiredToken,
        UnsufficentRights,
        Error
    }

    public enum AnnounceState
    {
        None,
        Announced,
        Accepted,
        Rejected,
        Error
    }
}
