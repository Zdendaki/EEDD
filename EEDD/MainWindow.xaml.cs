using Communication.Data;
using Communication.Procedures;
using Communication.Procedures.Clients;
using EEDD.Form;
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
        private static EDDControl? focused = null;
        public static EDDControl? FocusedControl
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
        bool canClose = false;

        public MainWindow()
        {
            InitializeComponent();

            //client = App.Data.Client;

            // Init window
            //Title = $"DOPRAVNÍ DENÍK - [{client.Name} - {client.User.Name}]";
            TableScale.ScaleX = TableScale.ScaleY = 1d;
            //InitHeader();
            InitRows();

            App.Client.MessageReceived += MessageReceived;
        }

        private void MessageReceived(Procedure procedure)
        {
            if (procedure is null)
                return;
            
            if (procedure is EndShiftResponse)
            {
                canClose = (procedure as EndShiftResponse)!.ResponseState == ResponseState.Success;
                if (canClose)
                    Close();
            }
        }
        
        private void InitHeader()
        {
            Header head = RowHeader;
            int i = 1;
            foreach(var sig in App.Data.Signallers)
            {
                if (i > 4)
                    break;
                else if (i == 1)
                {
                    head.Sig1.Text = sig.Key.name.Truncate(2).Insert(1, Environment.NewLine);
                    head.Sig1.Width = 15;
                }
                else if (i == 2)
                {
                    head.Sig2.Text = sig.Key.name.Truncate(2).Insert(1, Environment.NewLine);
                    head.Sig2.Width = 15;
                }
                else if (i == 3)
                {
                    head.Sig3.Text = sig.Key.name.Truncate(2).Insert(1, Environment.NewLine);
                    head.Sig3.Width = 15;
                }
                else if (i == 4)
                {
                    head.Sig4.Text = sig.Key.name.Truncate(2).Insert(1, Environment.NewLine);
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
            Rows.Children.Add(new RowComment());
            Rows.Children.Add(new RowTrainDouble());
            Rows.Children.Add(new RowTrainDouble());
            Rows.Children.Add(new RowTrainDouble());
        }

        private void InitScale()
        {
            PresentationSource source = PresentationSource.FromVisual(this);

            if (source is not null)
            {
                double scaleX = 1d + (source.CompositionTarget.TransformToDevice.M11 - 1d) / 2d;
                double scaleY = 1d + (source.CompositionTarget.TransformToDevice.M22 - 1d) / 2d;

                TableScale.ScaleX = TableScale.ScaleY = Math.Max(scaleX, scaleY);
            }
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
            if (canClose)
                return;
            
            var result = MessageBox.Show(this, "Přejete si ukončit směnu?", "Ukončení směny", MessageBoxButton.YesNo, MessageBoxImage.Question);

            e.Cancel = true;

            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
                Task.Run(() =>
                {
                    App.Client.SendMessage(new EndShiftRequest(App.Data.ShiftId));
                });
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitScale();
        }
    }
}
