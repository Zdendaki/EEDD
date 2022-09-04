using Communication.Data;
using Communication.Procedures;
using ServerData.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Builders
{
    internal class ClientDataBuilder
    {
        Context db;
        Shift shift;
        Client client;
        User user;
        
        public ClientDataBuilder(Context context, int shiftId, User user)
        {
            db = context;
            shift = db.Shifts.Single(x => x.Id == shiftId);
            client = shift.Client;
            this.user = user;
        }

        public ClientData BuildClientData()
        {
            var stationData = GetStations();
            return new ClientData(client.Id, client.Name, new()/*GetRows(stationData)*/, GetStations(), GetTrains(), GetUser());
        }

        private List<RowData> GetRows(List<StationData> stationData)
        {
            List<RowData> data = new();
            foreach (var row in client.Stations.SelectMany(x => x.Archive).Where(x => !x.Cancelled && !x.RowComplete && (DateTime.Now - x.LastUpdate).TotalHours < 8d).OrderBy(x => x.Id))
            {
                if (row.RowType == RowType.Comment)
                    data.Add(GetMessageRow(row));
                else if (row.RowType == RowType.Arrival)
                    data.Add(GetArrivalRow(row, stationData.Single(x => x.Id == row.Station.Id)));
                else if (row.RowType == RowType.Departure)
                    data.Add(GetDepartureRow(row, stationData.Single(x => x.Id == row.Station.Id)));
                else if (row.RowType == RowType.Both)
                    data.Add(GetDoubleRow(row, stationData.Single(x => x.Id == row.Station.Id)));
                else
                    throw new Exception("Undefined row type " + row.RowType.ToString());
            }
            return data;
        }

        private MessageRow GetMessageRow(Row row)
        {
            return new MessageRow(row.Id, row.RowType, RowColor.Red, row.ResponsibleUser.Id != user.Id, row.RowChar, row.Caption!, row.Message!.GetValue(), RowDataString.GetValue(row.NoteA)); //TODO: Opravit
        }

        private SingleTrainRow GetArrivalRow(Row row, StationData station)
        {
            return new SingleTrainRow(row.Id, row.RowType, row.ResponsibleUser.Id != user.Id, GetTrainData(row.Train), row.NumberA!.Value, row.TypeA!.Value, row.RouteA!)
            {
                Accepted = RowDataDate.GetValue(row.AcceptedA),
                ActualDeparture = RowDataDate.GetValue(row.ActualDepA),
                Announced = RowDataDate.GetValue(row.AnnouncedA),
                Approval = row.ApprovalA,
                Cancelled = row.Cancelled,
                Comment = RowDataString.GetValue(row.CommentA),
                Complete = row.RowComplete,
                Delay = row.ADelay,
                Departed = RowDataDate.GetValue(row.DepartedA),
                PMD = RowDataDate.GetValue(row.APMD),
                Exceptions = row.ExceptionsA,
                Note = RowDataString.GetValue(row.NoteA),
                SentMessages = row.SentMessagesA,
                Sig1 = RowDataSignaller.GetValue(row.Sig1A, station),
                Sig2 = RowDataSignaller.GetValue(row.Sig2A, station),
                Sig3 = RowDataSignaller.GetValue(row.Sig3A, station),
                Sig4 = RowDataSignaller.GetValue(row.Sig4A, station),
                Time = RowDataDate.GetValue(row.Arrival),
                Track = RowDataTrack.GetValue(row.TrackA),
                Delays = GetDelays(row, true),
                Train = GetTrainData(row.Train)
            };
        }

        private SingleTrainRow GetDepartureRow(Row row, StationData station)
        {
            return new SingleTrainRow(row.Id, row.RowType, row.ResponsibleUser.Id != user.Id, GetTrainData(row.Train), row.NumberD!.Value, row.TypeD!.Value, row.RouteD!)
            {
                Accepted = RowDataDate.GetValue(row.AcceptedD),
                ActualDeparture = RowDataDate.GetValue(row.ActualDepD),
                Announced = RowDataAcception.GetValue(row.AnnouncedD),
                AcceptionState = row.AnnouncedD?.State,
                Approval = row.ApprovalD,
                Cancelled = row.Cancelled,
                Comment = RowDataString.GetValue(row.CommentD),
                Complete = row.RowComplete,
                Delay = row.DDelay,
                Departed = RowDataDate.GetValue(row.DepartedD),
                PMD = RowDataDate.GetValue(row.DPMD),
                Exceptions = row.ExceptionsD,
                Note = RowDataString.GetValue(row.NoteD),
                SentMessages = row.SentMessagesD,
                Sig1 = RowDataSignaller.GetValue(row.Sig1D, station),
                Sig2 = RowDataSignaller.GetValue(row.Sig2D, station),
                Sig3 = RowDataSignaller.GetValue(row.Sig3D, station),
                Sig4 = RowDataSignaller.GetValue(row.Sig4D, station),
                Time = RowDataDate.GetValue(row.Departure),
                Track = RowDataTrack.GetValue(row.TrackD),
                Delays = GetDelays(row, false),
                Train = GetTrainData(row.Train)
            };
        }

        private DoubleTrainRow GetDoubleRow(Row row, StationData station)
        {
            return new DoubleTrainRow(row.Id, row.RowType, row.ResponsibleUser.Id != user.Id, GetTrainData(row.Train), row.NumberA!.Value, row.NumberD!.Value, row.TypeA!.Value, row.TypeD!.Value, row.RouteA!, row.RouteD!)
            {
                Accepted = RowDataDate.GetValue(row.AcceptedA, row.AcceptedD),
                ActualDeparture = RowDataDate.GetValue(row.ActualDepA, row.ActualDepD),
                Announced = RowDataAcception.GetValue(row.AnnouncedA, row.AnnouncedD),
                AcceptionState = row.AnnouncedD?.State,
                ApprovalA = row.ApprovalA,
                ApprovalD = row.ApprovalD,
                Cancelled = row.Cancelled,
                Comment = RowDataString.GetValue(row.CommentA, row.CommentD),
                Complete = row.RowComplete,
                DelayA = row.ADelay,
                DelayD = row.DDelay,
                Departed = RowDataDate.GetValue(row.DepartedA, row.DepartedD),
                PMD = RowDataDate.GetValue(row.APMD, row.DPMD),
                ExceptionsA = row.ExceptionsA,
                ExceptionsD = row.ExceptionsD,
                Note = RowDataString.GetValue(row.NoteA, row.NoteD),
                SentMessagesA = row.SentMessagesA,
                SentMessagesD = row.SentMessagesD,
                Sig1 = RowDataSignaller.GetValue(row.Sig1A, row.Sig1D, station),
                Sig2 = RowDataSignaller.GetValue(row.Sig2A, row.Sig2D, station),
                Sig3 = RowDataSignaller.GetValue(row.Sig3A, row.Sig3D, station),
                Sig4 = RowDataSignaller.GetValue(row.Sig4A, row.Sig4D, station),
                Time = RowDataDate.GetValue(row.Arrival, row.Departure),
                Track = RowDataTrack.GetValue(row.TrackA, row.TrackD),
                DelaysA = GetDelays(row, true),
                DelaysD = GetDelays(row, false),
                Train = GetTrainData(row.Train)
            };
        }

        private List<RowDelayValue> GetDelays(Row row, bool arrival)
        {
            List<RowDelayValue> data = new();
            if (arrival)
            {
                foreach (var delay in row.DelaysA) 
                {
                    RowDelayValue val = new RowDelayValue(delay.Id, delay.Reason, delay.Minutes, delay.TrainNumber, delay.Description);
                    data.Add(val);
                }
            }
            else
            {
                foreach (var delay in row.DelaysD)
                {
                    RowDelayValue val = new RowDelayValue(delay.Id, delay.Reason, delay.Minutes, delay.TrainNumber, delay.Description);
                    data.Add(val);
                }
            }
            return data;
        }

        #region Station
        private List<StationData> GetStations()
        {
            List<StationData> data = new();
            foreach (Station station in client.Stations)
            {
                StationData sd = new StationData(station.Id, station.Name, station.Abbr, station.TimePenalty, station.Color, GetConnections(station), GetStationTracks(station), GetSignallers(station));
                data.Add(sd);
            }
            return data;
        }

        private List<ConnectionData> GetConnections(Station station)
        {
            List<ConnectionData> data = new();
            foreach (var connection in db.Connections.Where(x => x.PrimaryId == station.Id || x.SecondaryId == station.Id))
            {
                bool primary = connection.PrimaryId == station.Id;
                ConnectionData cd = new ConnectionData(connection.Id, primary ? connection.SecondaryId : connection.PrimaryId, primary ? connection.Secondary.Abbr : connection.Primary.Abbr, GetConnectionTracks(connection, primary));
                data.Add(cd);
            }
            return data;
        }

        private List<ConnectionData.Track> GetConnectionTracks(StationConnection connection, bool primary)
        {
            List<ConnectionData.Track> data = new();
            foreach (var track in connection.RouteTracks)
            {
                DefaultDirection direction = DefaultDirection.None;

                if ((track.Direction == DefaultDirection.PrimarySecondary && primary) || (track.Direction == DefaultDirection.SecondaryPrimary && !primary))
                    direction = DefaultDirection.PrimarySecondary;
                else if ((track.Direction == DefaultDirection.SecondaryPrimary && primary) || (track.Direction == DefaultDirection.PrimarySecondary && !primary))
                    direction = DefaultDirection.SecondaryPrimary;

                ConnectionData.Track ct = new ConnectionData.Track(track.Id, track.Number, track.Interlocking, direction, track.MinimumInterval);
                data.Add(ct);
            }
            return data;
        }

        private List<StationData.Track> GetStationTracks(Station station)
        {
            List<StationData.Track> data = new();
            foreach (var track in station.Tracks)
            {
                StationData.Track st = new StationData.Track(track.Id, track.Name, track.Platform);
                data.Add(st);
            }
            return data;
        }

        private List<StationData.Signaller> GetSignallers(Station station)
        {
            List<StationData.Signaller> data = new();
            foreach (var signaller in station.Signallers.Where(x => (x.ValidFrom is null || x.ValidFrom.Value.Date <= DateTime.Today) && (x.ValidTo is null || x.ValidTo.Value.Date >= DateTime.Today.AddDays(1))))
            {
                StationData.Signaller ss = new StationData.Signaller(signaller.Id, signaller.Name, signaller.Comment, signaller.Type, signaller.Order);
                data.Add(ss);
            }
            return data;
        }
        #endregion

        private List<TrainData> GetTrains()
        {
            return new(); //TODO: get trains
        }

        private TrainData? GetTrainData(Train? train)
        {
            return new();
        }

        private UserData GetUser()
        {
            return new UserData(user.Id, user.Username, user.Name, user.Role);
        }
    }
}
