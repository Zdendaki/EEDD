using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        FieldType fieldType = FieldType.Static;
        bool focus = false;
        
        public string Text
        {
            get => text;
            set
            {
                bool flag = text != value;
                text = value.Limit(MaxLength);
                if (flag)
                    OnPropertyChanged(nameof(Text));
            }
        }

        public int MaxLength
        {
            get => maxLength;
            set
            {
                int len = Math.Min(GetMaxLength(fieldType), value);
                bool flag = maxLength != len;
                maxLength = len;
                if (flag)
                    OnPropertyChanged(nameof(MaxLength));
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
                TextLabel.Foreground = editMode == EditState.Locked ? EDDBrushes.Gray : Brushes.Black;
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

        public FieldType Type
        {
            get => fieldType;
            set
            {
                fieldType = value;
                MaxLength = Math.Min(maxLength, GetMaxLength(value));
                Background = value == FieldType.Note ? Brushes.White : Brushes.Transparent;
            }
        }

        public bool HasFocus
        {
            get => focus;
            set
            {
                focus = value;
                FocusBorder.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Row? Row { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public EDDTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void Focus(bool force)
        {
            if (force && EditMode != EditState.Locked)
                EditMode = EditState.CanEdit;
            if (TextBox.Visibility == Visibility.Visible)
                TextBox.Focus();
            MainWindow.FocusedTextBox = this;
            HasFocus = true;
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void UserControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Focus(false);
        }

        private void UserControl_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Type == FieldType.AnnounceTime)
                {
                    if (!ParseTime())
                        return;

                    EditMode = EditState.CanModify;
                    RequestArrow.Visibility = Visibility.Visible;
                    Task.Run(() =>
                    {
                        Thread.Sleep(4000);
                        Dispatcher.Invoke(() => { RequestArrow.Visibility = Visibility.Collapsed; AcceptRectangle.Visibility = Visibility.Visible; });
                    });
                }
                else if (Type == FieldType.Time)
                {
                    if (!ParseTime())
                        return;

                    EditMode = EditState.CanModify;
                }
                else if (Type == FieldType.Note)
                {

                }
                else
                {
                    EditMode = EditState.CanModify;
                }

                Row?.Next(this);
            }
            else if (e.Key == Key.Tab)
            {
                Row?.Next(this, true);
            }
            else
            {
                
            }
        }

        private bool ParseTime()
        {
            string text = TextBox.Text;

            if (text.Length == 4 && !text.Contains(':'))
                text = text.Insert(2, ":");

            string output = "";
            int i = 0;

            foreach (string txt in text.Split(':'))
            {
                if (!int.TryParse(txt, out int x) || (i == 0 && x > 24) || (i == 1 && x > 59) || x < 0)
                    return false;

                output += txt.PadLeft(2, '0');

                if (i == 0)
                    output += ':';

                i++;
            }

            if (output.Length != 5)
                return false;

            Text = output;
            return true;
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextBox.Text.Length > 0)
            {
                TextBox.SelectAll();
            }
        }

        private void TextBox_LostMouseCapture(object sender, MouseEventArgs e)
        {
            if (TextBox.Text.Length > 0)
            {
                TextBox.SelectAll();
            }
        }

        private int GetMaxLength(FieldType type)
        {
            switch (type)
            {
                case FieldType.TrainNumber:
                    return 6;
                case FieldType.AnnounceTime:
                case FieldType.Time:
                case FieldType.Number:
                    return 5;
                case FieldType.SignallerTime:
                case FieldType.Note:
                    return 1;
                default:
                    return 50;
            }
        }
    }

    public enum EditState
    {
        CanEdit,
        CanModify,
        Locked
    }

    public enum FieldType
    {
        Static,
        TrainNumber,
        Number,
        Time,
        AnnounceTime,
        String,
        SignallerTime,
        Note,

    }
}
