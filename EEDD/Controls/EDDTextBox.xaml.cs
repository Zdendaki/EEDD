using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EEDD.Controls
{
    /// <summary>
    /// Interakční logika pro EDDTextBox.xaml
    /// </summary>
    public partial class EDDTextBox : EDDControl, INotifyPropertyChanged
    {
        bool focus = false;
        EditMode editMode;

        public new string Text
        {
            get => base.Text.Limit(MaxLength);
            set => base.Text = value;
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

        public EditMode EditMode
        {
            get => editMode;
            set
            {
                bool flag = editMode != value;
                editMode = value;
                if (flag)
                    textBox.Visibility = value == EditMode.CanEdit ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public FieldType Type { get; init; }

        public EDDTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void Focus(bool force)
        {
            if (force && EditMode != EditMode.Locked)
                EditMode = EditMode.CanEdit;
            if (textBox.Visibility == Visibility.Visible)
                textBox.Focus();
            MainWindow.FocusedControl = this;
            HasFocus = true;
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
                if (Type == FieldType.Time && Name == "dAnnounced") // TODO: temporary
                {
                    if (!ParseTime())
                        return;

                    EditMode = EditMode.CanModify;
                    requestArrow.Visibility = Visibility.Visible;
                    Task.Run(() =>
                    {
                        Thread.Sleep(4000);
                        Dispatcher.Invoke(() => { requestArrow.Visibility = Visibility.Collapsed; acceptRectangle.Visibility = Visibility.Visible; });
                    });
                }
                else if (Type == FieldType.Time)
                {
                    if (!ParseTime())
                        return;

                    EditMode = EditMode.CanModify;
                }
                else
                {
                    EditMode = EditMode.CanModify;
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
            string text = textBox.Text;

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
            if (textBox.Text.Length > 0)
            {
                textBox.SelectAll();
            }
        }

        private void TextBox_LostMouseCapture(object sender, MouseEventArgs e)
        {
            if (textBox.Text.Length > 0)
            {
                textBox.SelectAll();
            }
        }
    }
}
