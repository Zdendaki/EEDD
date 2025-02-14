using Common.Data;
using EEDD.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace EEDD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool canClose = false;

        public MainWindow()
        {
            InitializeComponent();

            // Init window
            Title = $"DOPRAVNÍ DENÍK - [{App.ClientData.Name} - {App.ClientData.User!.Name}]";
            TableScale.ScaleX = TableScale.ScaleY = 1d;

            //InitHeader();
            InitRows();
        }

        private void InitRows()
        {
            Rows.Children.Add(new RowComment());
            Rows.Children.Add(new RowTrain(StationColor.Gray, true));
            Rows.Children.Add(new RowTrain(StationColor.Gray, false));
            Rows.Children.Add(new RowTrain(StationColor.Yellow, true));
            Rows.Children.Add(new RowTrain(StationColor.Yellow, false));
            Rows.Children.Add(new RowTrain(StationColor.Green, false));
        }

        private void InitScale()
        {
            PresentationSource source = PresentationSource.FromVisual(this);

            if (source is not null)
            {
                double scaleX = source.CompositionTarget.TransformToDevice.M11;
                double scaleY = source.CompositionTarget.TransformToDevice.M22;

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
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitScale();
        }
    }
}
