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
    /// Interakční logika pro EDDTextBox.xaml
    /// </summary>
    public partial class EDDTextBox : UserControl, INotifyPropertyChanged
    {
        string text = "12:49";
        int maxLength = 6;
        HorizontalAlignment align = HorizontalAlignment.Center;
        EditState editMode = EditState.Locked;
        Brush? innerBorder = null;
        
        public string Text
        {
            get => text;
            set
            {
                if (text != value)
                    OnPropertyChanged(nameof(Text));
                text = value;
            }
        }

        public int MaxLength
        {
            get => maxLength;
            set
            {
                if (maxLength != value)
                    OnPropertyChanged(nameof(MaxLength));
                maxLength = value;
            }
        }
        
        public HorizontalAlignment Align
        {
            get => align;
            set
            {
                TextBox.HorizontalContentAlignment = value;
                switch (value)
                {
                    case HorizontalAlignment.Left:
                        TextLabel.Style = (Style)FindResource("LabelLeft");
                        break;
                    case HorizontalAlignment.Center:
                        TextLabel.Style = (Style)FindResource("LabelCenter");
                        break;
                    case HorizontalAlignment.Right:
                        TextLabel.Style = (Style)FindResource("LabelRight");
                        break;
                }
            }
        }

        public EditState EditMode
        {
            get => editMode;
            set 
            {
                editMode = value;
                TextBox.Visibility = editMode == EditState.CanEdit ? Visibility.Visible : Visibility.Collapsed;
                TextLabel.Foreground = editMode == EditState.Locked ? Brushes.Gray : Brushes.Black;
                Cursor = editMode == EditState.CanEdit ? Cursors.IBeam : Cursors.Arrow;
            }
        }

        public Brush? InnerBorder
        {
            get => innerBorder;
            set
            {
                innerBorder = value;
                InBorder.Visibility = value is null ? Visibility.Collapsed : Visibility.Visible;
                if (value != null)
                    InBorder.BorderBrush = value;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public EDDTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void UserControl_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }

    public enum EditState
    {
        CanEdit,
        CanModify,
        Locked
    }
}
