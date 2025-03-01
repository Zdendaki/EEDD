using Common.Data;

namespace EVAL.ViewModels
{
    public class TrainRouteVM : ViewModel
    {
        public uint ID { get; init; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private RouteTrack track;
        public RouteTrack Track
        {
            get => track;
            set
            {
                track = value;
                OnPropertyChanged(nameof(Track));
            }
        }

        private string comment;
        public string Comment
        {
            get => comment;
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
    }
}
