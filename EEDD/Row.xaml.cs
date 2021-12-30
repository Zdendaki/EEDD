using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EEDD
{
    /// <summary>
    /// Interakční logika pro Row.xaml
    /// </summary>
    public partial class Row : UserControl
    {
        public Row(StationBackground station, bool odd)
        {
            InitializeComponent();

            Background = GetBackground(station, odd);
        }

        public Row() : this(StationBackground.Gray, true) { }

        internal void Next(EDDTextBox prev, bool tab = false)
        {
            string name = prev.Name;

            if (name == nameof(ATrack))
                Arrival.Focus(!tab);
            if (name == nameof(Arrival))
                DAnnounced.Focus(false);
            else if (name == nameof(DAnnounced))
                DTrack.Focus(!tab);
            else if (name == nameof(DTrack))
                Departure.Focus(!tab);
        }

        private Brush GetBackground(StationBackground station, bool odd)
        {
            if (station == StationBackground.Gray)
                return odd ? EDDBrushes.BackgroundGray1 : EDDBrushes.BackgroundGray2;
            else if (station == StationBackground.Green)
                return odd ? EDDBrushes.BackgroundGreen1 : EDDBrushes.BackgroundGreen2;
            else if (station == StationBackground.Yellow)
                return odd ? EDDBrushes.BackgroundYellow1 : EDDBrushes.BackgroundYellow2;
            else throw new ArgumentException("Station's background is undefined", nameof(station));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (EDDTextBox tb in this.FindVisualChildren<EDDTextBox>())
            {
                tb.Row = this;
            }
        }
    }
}
