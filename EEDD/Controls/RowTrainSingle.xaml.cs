using Common.Data;
using System;
using System.Windows;

namespace EEDD.Controls
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
