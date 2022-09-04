using Communication.Data;
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

namespace EEDD.Form
{
    /// <summary>
    /// Interakční logika pro Row.xaml
    /// </summary>
    public partial class RowTrainDouble : Row
    {        
        public RowTrainDouble(StationColor color) : base(color, RowType.Both)
        {
            InitializeComponent();
        }

        public RowTrainDouble() : this(StationColor.Gray) { }

        public override void Next(EDDTextBox prev, bool tab = false)
        {
            string name = prev.Name;

            if (name == nameof(aTrack))
                arrival.Focus(!tab);
            if (name == nameof(arrival))
                dAnnounced.Focus(false);
            else if (name == nameof(dAnnounced))
                dTrack.Focus(!tab);
            else if (name == nameof(dTrack))
                departure.Focus(!tab);
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
