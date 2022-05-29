using Communication.Procedures.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static EDDTextBox? focused = null;
        public static EDDTextBox? FocusedTextBox
        {
            get => focused;
            set
            {
                if (focused is not null)
                    focused.HasFocus = false;
                focused = value;
            }
        }
        
        public MainWindow()
        {
            InitializeComponent();

            TableScale.ScaleX = TableScale.ScaleY = 1.5f;

            Rows.Children.Add(new Row(StationBackground.Gray, true));
            Rows.Children.Add(new Row(StationBackground.Gray, false));
            Rows.Children.Add(new Row(StationBackground.Gray, true));
        }

        private void LabelZoomIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TableScale.ScaleX *= 1.1d;
            TableScale.ScaleY *= 1.1d;
        }

        private void LabelZoomOut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TableScale.ScaleX /= 1.1d;
            TableScale.ScaleY /= 1.1d;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var result = MessageBox.Show(this, "Opravdu chcete ukončit dopravní deník?", "Ukončení směny", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = true;
                Task.Run(() =>
                {
                    App.Client.SendMessage(new EndShiftRequest(App.Data.ShiftId));
                });
            }
        }
    }
}
