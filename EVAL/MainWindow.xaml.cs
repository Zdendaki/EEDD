using Common.Data;
using EVAL.Data;
using EVAL.Windows;
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
                    new() { ID = 336222, Type = TrainType.Os, Number = 1, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                    new() { ID = 349845, Type = TrainType.Os, Number = 1, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                    new() { ID = 333344, Type = TrainType.Os, Number = 1, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                    new() { ID = 339341, Type = TrainType.Os, Number = 1, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                    new() { ID = 333542, Type = TrainType.Os, Number = 1, Arrival = DateTime.Now, Departure = DateTime.Now, TrackArrival = "1", TrackDeparture = "1", RouteTrackArrival = "1", RouteTrackDeparture = "1", StartBetweenStations = false, EndBetweenStations = false },
                ];

            Train t1 = new()
            {
                ID = Guid.NewGuid(),
                Number = "1",
                Date = DateTime.Today,
                Stops = stops
            };
            Train t2 = new()
            {
                ID = Guid.NewGuid(),
                Number = "1",
                Date = DateTime.Today,
                Stops = stops
            };
            Train t3 = new()
            {
                ID = Guid.NewGuid(),
                Number = "1",
                Date = DateTime.Today,
                Stops = stops
            };
            Train t4 = new()
            {
                ID = Guid.NewGuid(),
                Number = "1",
                Date = DateTime.Today,
                Stops = stops
            };
            Train t5 = new()
            {
                ID = Guid.NewGuid(),
                Number = "1",
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

        private void DataGridRow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is DataGridRow row)
            {
                row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void TrainsGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not DataGrid grid)
                return;

            if (ItemsControl.ContainerFromElement(grid, e.OriginalSource as DependencyObject) is null)
                return;

            DataGridRow row;
            if (grid.SelectedItem is DataGridCell)
            {
                DependencyObject parent = VisualTreeHelper.GetParent((DataGridCell)grid.SelectedItem);
                if (parent is not DataGridRow)
                    return;

                row = (DataGridRow)parent;
            }
            else if (grid.SelectedItem is DataGridRow)
                row = (DataGridRow)grid.SelectedItem;
            else
                return;

            row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void NewTrain_Click(object sender, RoutedEventArgs e)
        {
            TrainWindow tw = new(null);
            tw.Owner = this;
            tw.ShowDialog();
        }
    }
}