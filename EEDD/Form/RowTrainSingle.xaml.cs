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
    public partial class RowTrainSingle : Row
    {       
        public RowTrainSingle(StationColor color, RowType type) : base(color, type)
        {
            if (type != RowType.Arrival && type != RowType.Departure)
                throw new InvalidOperationException("Invalid row type");
            
            InitializeComponent();
        }

        public RowTrainSingle() : this(StationColor.Gray, RowType.Arrival) { }

        public override void Next(EDDTextBox prev, bool tab = false)
        {
            string name = prev.Name;

            if (name == nameof(track))
                time.Focus(!tab);
            if (name == nameof(time))
                announced.Focus(false);
            else if (name == nameof(announced))
                track.Focus(!tab);
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
