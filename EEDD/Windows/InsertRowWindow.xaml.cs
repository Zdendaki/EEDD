using System.Windows;

namespace EEDD.Windows
{
    /// <summary>
    /// Interakční logika pro InsertRowWindow.xaml
    /// </summary>
    public partial class InsertRowWindow : Window
    {
        public InsertRowWindow()
        {
            InitializeComponent();
        }

        private void TextSelect_Checked(object sender, RoutedEventArgs e)
        {
            TextType.IsEnabled = TextSelect.IsChecked == true;
        }

        private void TrainSelect_Checked(object sender, RoutedEventArgs e)
        {
            TrainNumber.IsEnabled = TrainSelect.IsChecked == true;
            if (TrainSelect.IsChecked != true)
                TrainNumber.Text = string.Empty;
        }
    }
}
