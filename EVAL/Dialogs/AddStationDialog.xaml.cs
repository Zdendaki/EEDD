using Common;
using Common.Data;
using Common.Data.Static;
using EVAL.Data;
using ModernWpf.Controls;

namespace EVAL.Dialogs
{
    /// <summary>
    /// Interakční logika pro AddStationDialog.xaml
    /// </summary>
    public partial class AddStationDialog : ContentDialog
    {
        private readonly Station _station;

        public TrainStop? Stop { get; init; }

        public AddStationDialog(Station station)
        {
            InitializeComponent();

            Title += station.Name;
            _station = station;

            category.ItemsSource = TrainHelper.GetCategoryItems();
            arrTrack.ItemsSource = depTrack.ItemsSource = station.Tracks;
            actions.ItemsSource = new TrainActions().OrderBy(x => x.Name).Select(x => new StopAction(x));
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs e)
        {
            if (category.SelectedIndex == -1)
            {
                e.Cancel = true;
                return;
            }

            if (!uint.TryParse(number.Text, out uint num))
            {
                e.Cancel = true;
                return;
            }

            if (!TrainHelper.IsValidTrainNumber(num))
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
