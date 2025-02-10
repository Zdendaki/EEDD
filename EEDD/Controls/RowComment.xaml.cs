using Common.Data;
using EEDD.Controls;
using System.Windows;

namespace EEDD.Controls
{
    /// <summary>
    /// Interakční logika pro Row.xaml
    /// </summary>
    public partial class RowComment : Row
    {
        public RowComment() : base(StationColor.Gray, RowType.Comment)
        {
            InitializeComponent();
        }

        public override void Next(EDDTextBox prev, bool tab = false)
        {
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
