using Common.Data;
using EVAL.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EVAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Title = $"EVAL [přihlášený uživatel {App.User?.Name}]";

            List<TrainStop> stops = 
                [
                    new() { ID = 336222, TypeArrival = TrainType.Os, TypeDeparture = TrainType.Os, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                    new() { ID = 349845, TypeArrival = TrainType.Os, TypeDeparture = TrainType.Os, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                    new() { ID = 333344, TypeArrival = TrainType.Os, TypeDeparture = TrainType.Os, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                    new() { ID = 339341, TypeArrival = TrainType.Os, TypeDeparture = TrainType.Os, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                    new() { ID = 333542, TypeArrival = TrainType.Os, TypeDeparture = TrainType.Os, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                ];

            Train t1 = new()
            {
                ID = Guid.NewGuid(),
                Number = 1,
                Date = DateTime.Today,
                Stops = stops
            };
            Train t2 = new()
            {
                ID = Guid.NewGuid(),
                Number = 2,
                Date = DateTime.Today,
                Stops = stops
            };
            Train t3 = new()
            {
                ID = Guid.NewGuid(),
                Number = 1,
                Date = DateTime.Today,
                Stops = stops
            };
            Train t4 = new()
            {
                ID = Guid.NewGuid(),
                Number = 1,
                Date = DateTime.Today,
                Stops = stops
            };
            Train t5 = new()
            {
                ID = Guid.NewGuid(),
                Number = 1,
                Date = DateTime.Today,
                Stops = stops
            };

            List<TrainEx> trains = [];
            trains.Add(new(t1));
            trains.Add(new(t2));
            trains.Add(new(t3));
            trains.Add(new(t4));
            trains.Add(new(t5));

            TrainsGrid.ItemsSource = trains;
        }

        private void Expander_Changed(object sender, RoutedEventArgs e)
        {
            for (Visual? vis = sender as Visual; vis is not null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            {
                if (vis is DataGridRow row)
                {
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
            }
        }
    }
}