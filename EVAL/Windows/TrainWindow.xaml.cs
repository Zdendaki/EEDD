using Common;
using Common.Data;
using Common.Data.Helper;
using Common.Data.Static;
using EVAL.Data;
using EVAL.Dialogs;
using System.Windows;

namespace EVAL.Windows
{
    /// <summary>
    /// Interakční logika pro TrainWindow.xaml
    /// </summary>
    public partial class TrainWindow : Window
    {
        public List<TrainTypeItem> Categories => TrainHelper.GetCategoryItems();

        public Train Train { get; init; }

        public TrainWindow(Train? train)
        {
            Train = train ?? Train.CreateNew();

            InitializeComponent();

            category.ItemsSource = TrainHelper.GetCategoryItems();
            actions.ItemsSource = new TrainActions().OrderBy(x => x.Name).Select(x => new StopAction(x));

            UpdateData();
        }

        private void UpdateData()
        {
            Train.Number = GetTrainNumber();
            nextStation.ItemsSource = UpdateStationsSelect();
        }

        private string GetTrainNumber()
        {
            if (!Train.Stops.Any())
                return string.Empty;

            return string.Join('/', Train.Stops.GroupBy(x => x.Number).Select(x => x.Key));
        }

        private IEnumerable<Station> UpdateStationsSelect()
        {
            if (!Train.Stops.Any())
                return App.Route.Stations.OrderBy(x => x.Name);

            Station last = App.Route.Stations.Single(x => x.ID == Train.Stops.Last().ID);

            if (Train.Stops.Count == 1)
            {
                return App.Route.StationConnections
                    .Where(sc => sc.Station1 == last.ID || sc.Station2 == last.ID)
                    .Select(sc => sc.Station1 == last.ID ? sc.Station2 : sc.Station1)
                    .Select(id => App.Route.Stations.Single(s => s.ID == id))
                    .OrderBy(x => x.Name);
            }

            Station beforeLast = App.Route.Stations.Single(x => x.ID == Train.Stops[^2].ID);

            return App.Route.StationConnections
                .Where(sc => sc.Station1 == last.ID || sc.Station2 == last.ID)
                .Where(sc => (sc.Station1 != last.ID && sc.Station2 != beforeLast.ID) && (sc.Station1 != beforeLast.ID && sc.Station2 != last.ID))
                .Select(sc => sc.Station1 == last.ID ? sc.Station2 : sc.Station1)
                .Select(id => App.Route.Stations.Single(s => s.ID == id))
                .OrderBy(x => x.Name);
        }

        private async void AddStation_Click(object sender, RoutedEventArgs e)
        {
            if (nextStation.SelectedItem is not Station station)
                return;

            AddStationDialog asd = new(station);
            await asd.ShowAsync();
        }
    }
}
