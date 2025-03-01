using Common.Data;
using System.ComponentModel;

namespace EVAL.ViewModels
{
    public class TrainVM : ViewModel
    {
        private string number;
        public string Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value.Date;
                OnPropertyChanged(nameof(Date));
            }
        }

        private bool startsOnRoute = false;
        public bool StartsOnRoute
        {
            get => startsOnRoute;
            set
            {
                startsOnRoute = value;
                OnPropertyChanged(nameof(StartsOnRoute));
            }
        }

        private RouteTrack startsOnRouteTrack;
        public RouteTrack StartsOnRouteTrack
        {
            get => startsOnRouteTrack;
            set
            {
                startsOnRouteTrack = value;
                OnPropertyChanged(nameof(StartsOnRouteTrack));
            }
        }

        private bool endsOnRoute = false;
        public bool EndsOnRoute
        {
            get => endsOnRoute;
            set
            {
                endsOnRoute = value;
                OnPropertyChanged(nameof(EndsOnRoute));
            }
        }

        private RouteTrack endsOnRouteTrack;
        public RouteTrack EndsOnRouteTrack
        {
            get => endsOnRouteTrack;
            set
            {
                endsOnRouteTrack = value;
                OnPropertyChanged(nameof(EndsOnRouteTrack));
            }
        }

        public BindingList<TrainStopVM> Stops { get; } = [];

        public BindingList<TrainRouteVM> Routes { get; } = [];
    }
}
