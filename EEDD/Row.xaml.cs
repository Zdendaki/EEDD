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
        Brush odd = new SolidColorBrush(Color.FromRgb(220, 220, 220));
        Brush even = new SolidColorBrush(Color.FromRgb(200, 200, 200));

        public Row(bool isOdd)
        {
            InitializeComponent();

            Background = isOdd ? odd : even;
        }

        public Row() : this(true) { }

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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (EDDTextBox tb in this.FindVisualChildren<EDDTextBox>())
            {
                tb.Row = this;
            }
        }
    }
}
