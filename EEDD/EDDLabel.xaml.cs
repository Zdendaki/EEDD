using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace EEDD
{
    /// <summary>
    /// Interakční logika pro EDDTextBox.xaml
    /// </summary>
    public partial class EDDLabel : EDDControl, INotifyPropertyChanged
    {
        bool focus = false;
        bool delBorder = false;

        public new string Text
        {
            get => base.Text.Limit(MaxLength);
            set => base.Text = value;
        }

        public bool Delayed
        {
            get => delBorder;
            set
            {
                bool flag = delBorder != value;
                delBorder = value;
                if (flag)
                    delayBorder.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public override bool HasFocus
        {
            get => focus;
            set
            {
                focus = value;
                focusBorder.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public EDDLabel()
        {
            InitializeComponent();
            DataContext = this;
            PropertyChanged += EDDLabel_PropertyChanged;
        }

        private void EDDLabel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            
        }

        public void Focus(bool force)
        {
            MainWindow.FocusedControl = this;
            HasFocus = true;
        }

        private void UserControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Focus(false);
        }

        private void UserControl_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Text = "P";
            Background = EDDBrushes.Transparent;
            ForeColor = EDDBrushes.Black;
        }
    }
}
