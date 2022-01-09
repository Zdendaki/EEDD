using Communication.Procedures;
using ServerData;
using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Communication.Procedures;
using S = ServerData;

namespace Server
{
    internal static class Extensions
    {
        internal static TokenState CheckToken(this Context context, string token, UserRole role)
        {
            return CheckUser(context, token, role).token;
        }

        internal static TokenState CheckToken(this Context context, byte[] token, UserRole role)
        {
            return CheckUser(context, token, role).token;
        }

        internal static User? GetUser(this Context context, string token)
        {
            return context.Users.FirstOrDefault(x => x.Token == token);
        }

        internal static User? GetUser(this Context context, byte[] token)
        {
            string buffer = BitConverter.ToString(token).ToLower().Replace("-", null);
            return context.Users.FirstOrDefault(x => x.Token == buffer);
        }

        internal static (TokenState token, User? user) CheckUser(this Context context, string token, UserRole role)
        {
            User? user = context.GetUser(token);

            if (user is null || !user.TokenIssued.HasValue || user.IsBanned)
                return (TokenState.Invalid, user);
            else if ((DateTime.Now - user.TokenIssued.Value).TotalDays > 1)
                return (TokenState.Expired, user);
            else if (user.Role < role)
                return (TokenState.UnsufficentRights, user);
            else
                return (TokenState.Ok, user);
        }

        internal static (TokenState token, User? user) CheckUser(this Context context, byte[] token, UserRole role)
        {
            string buffer = BitConverter.ToString(token).ToLower().Replace("-", null);
            return CheckUser(context, buffer, role);
        }

        public static string GetString(this byte[] input)
        {
            return BitConverter.ToString(input).Replace("-", null).ToLower();
        }

        public static string Scramble(this string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }

        public static D.RowType? GetRowType(this S.RowType? rowType)
        {
            if (rowType is null)
                return null;
            else
                return rowType.Value.GetRowType();
        }

        public static D.RowType GetRowType(this S.RowType rowType)
        {
            return (D.RowType)(short)rowType;
        }

        public static D.TrainType? GetTrainType(this S.TrainType? trainType)
        {
            if (trainType is null)
                return null;
            else
                return trainType.Value.GetTrainType();
        }

        public static D.TrainType GetTrainType(this S.TrainType trainType)
        {
            return (D.TrainType)(short)trainType;
        }

        public static string GetAbbr(this RouteTrack track, IEnumerable<StationConnection> connections, Station station)
        {
            if (connections.Contains(track.Connection))
            {
                var conn = connections.Single(x => x.Id == track.Connection.Id);
                if (conn.Primary == station)
                    return conn.Secondary.Abbr;
                else
                    return conn.Primary.Abbr;
            }
            else
                return string.Empty;
        }

        internal static IEnumerable<Connection> GetConnections(this IEnumerable<StationConnection> connections, Station station)
        {
            foreach (var connection in connections.Where(x => x.Primary == station || x.Secondary == station))
            {
                List<Connection.Track> tracks = new List<Connection.Track>();
                foreach (var track in connection.RouteTracks)
                    tracks.Add(new Connection.Track(track.Id, track.Number, track.Interlocking, track.MinimumInterval));

                if (connection.Primary == station)
                    yield return new Connection(connection.Id, connection.SecondaryId, connection.Secondary.Abbr, tracks);
                else
                    yield return new Connection(connection.Id, connection.PrimaryId, connection.Primary.Abbr, tracks);
            }
        }

        internal static RowData GetRowData(this Row data, Context context)
        {
            if ((int)data.RowType > 2)
            {
                return new RowData(data.Id, data.Train?.Id, !data.RowComplete, data.RowType.GetRowType(), data.Caption, data.Message);
            }
            else
            {
                return new();
            }
        }
    }
}
