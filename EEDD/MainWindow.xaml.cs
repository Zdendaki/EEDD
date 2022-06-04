using Communication.Procedures;
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

        readonly ClientData client;

        public MainWindow()
        {
            InitializeComponent();

            client = App.Data.Client;

            // Init window
            Title = $"DOPRAVNÍ DENÍK - [{client.Name} - {client.User.Name}]";
            TableScale.ScaleX = TableScale.ScaleY = 1d;
            InitHeader();
            InitRows();


            Rows.Children.Add(new Row(StationBackground.Gray, true));
            Rows.Children.Add(new Row(StationBackground.Gray, false));
            Rows.Children.Add(new Row(StationBackground.Gray, true));
        }

        private void InitHeader()
        {
            Header head = RowHeader;
            int i = 1;
            foreach(StationData.Signaller sig in App.Data.Signallers)
            {
                if (i > 4)
                    break;
                else if (i == 1)
                {
                    head.Sig1.Text = sig.Name.Truncate(2).Insert(1, Environment.NewLine);
                    head.Sig1.Width = 15;
                }
                else if (i == 2)
                {
                    head.Sig2.Text = sig.Name.Truncate(2).Insert(1, Environment.NewLine);
                    head.Sig2.Width = 15;
                }
                else if (i == 3)
                {
                    head.Sig3.Text = sig.Name.Truncate(2).Insert(1, Environment.NewLine);
                    head.Sig3.Width = 15;
                }
                else if (i == 4)
                {
                    head.Sig4.Text = sig.Name.Truncate(2).Insert(1, Environment.NewLine);
                    head.Sig4.Width = 15;
                }
                i++;
            }

            if (i < 4)
                head.Sig4.Width = 0;
            if (i < 3)
                head.Sig3.Width = 0;
            if (i < 2)
                head.Sig2.Width = 0;
            if (i < 1)
                head.Sig1.Width = 0;
        }

        private void InitRows()
        {

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
                    App.Client.SendMessageAsync(new EndShiftRequest(App.Data.ShiftId));
                });
            }
        }
    }
}
