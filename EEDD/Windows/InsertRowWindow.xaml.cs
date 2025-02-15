using Common;
using System.Windows;

namespace EEDD.Windows
{
    /// <summary>
    /// Interakční logika pro InsertRowWindow.xaml
    /// </summary>
    public partial class InsertRowWindow : Window
    {
        readonly bool _initialized = false;

        public InsertRowWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;

            TrainNumber.Focus();
            _initialized = true;
        }

        private void TextSelect_Checked(object sender, RoutedEventArgs e)
        {
            TextType.IsEnabled = TextSelect.IsChecked == true;
        }

        private void TrainSelect_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            TrainNumber.IsEnabled = TrainSelect.IsChecked == true;
            if (TrainSelect.IsChecked != true)
                TrainNumber.Text = string.Empty;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (TrainSelect.IsChecked == true)
                SelectTrain();
            else if (PMDSelect.IsChecked == true)
                SelectPMD();
            else if (TextSelect.IsChecked == true)
                SelectText();
            else
                MessageBox.Show(this, "Vyberte typ řádku", "Elektronický dopravní deník", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SelectTrain()
        {
            if (string.IsNullOrWhiteSpace(TrainNumber.Text))
            {
                MessageBox.Show(this, "Zadejte číslo vlaku", "Elektronický dopravní deník", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!uint.TryParse(TrainNumber.Text.Trim(), out uint trainNumber) || !TrainHelper.IsValidTrainNumber(trainNumber))
            {
                MessageBox.Show(this, "Neplatné číslo vlaku", "Elektronický dopravní deník", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Close();
        }

        private void SelectPMD()
        {
            MessageBox.Show(this, "Nepodporovaná funkce", "Elektronický dopravní deník", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void SelectText()
        {
            if (TextType.SelectedIndex < 0)
            {
                MessageBox.Show(this, "Vyberte typ textu", "Elektronický dopravní deník", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Close();
        }

        private void Storno_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
