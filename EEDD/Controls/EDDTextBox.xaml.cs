using Common.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EEDD.Controls
{
    /// <summary>
    /// Interakční logika pro EDDTextBox.xaml
    /// </summary>
    public partial class EDDTextBox : UserControl
    {
        public static readonly DependencyProperty InnerBorderProperty = DependencyProperty.Register(nameof(InnerBorder), typeof(SolidColorBrush), typeof(EDDTextBox), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty InnerBorder2Property = DependencyProperty.Register(nameof(InnerBorder2), typeof(SolidColorBrush), typeof(EDDTextBox), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty AcceptionProperty = DependencyProperty.Register(nameof(Acception), typeof(SolidColorBrush), typeof(EDDTextBox), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty RequestProperty = DependencyProperty.Register(nameof(Request), typeof(SolidColorBrush), typeof(EDDTextBox), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(nameof(MaxLength), typeof(int), typeof(EDDTextBox), new PropertyMetadata(0));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(EDDTextBox), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty EditModeProperty = DependencyProperty.Register(nameof(EditMode), typeof(EditMode), typeof(EDDTextBox), new PropertyMetadata(EditMode.CanEdit, PropertyChanged));
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(nameof(TextAlignment), typeof(TextAlignment), typeof(EDDTextBox), new PropertyMetadata(TextAlignment.Left));

        Brush _foreground;

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        string caption;
        public string Caption
        {
            get => caption;
            set
            {
                caption = value;
                SetTooltip(value);
            }
        }

        public SolidColorBrush InnerBorder
        {
            get => (SolidColorBrush)GetValue(InnerBorderProperty);
            set => SetValue(InnerBorderProperty, value);
        }

        public SolidColorBrush InnerBorder2
        {
            get => (SolidColorBrush)GetValue(InnerBorder2Property);
            set => SetValue(InnerBorder2Property, value);
        }

        public SolidColorBrush Acception
        {
            get => (SolidColorBrush)GetValue(AcceptionProperty);
            set => SetValue(AcceptionProperty, value);
        }

        public SolidColorBrush Request
        {
            get => (SolidColorBrush)GetValue(RequestProperty);
            set => SetValue(RequestProperty, value);
        }

        public EditMode EditMode
        {
            get => (EditMode)GetValue(EditModeProperty);
            set
            {
                SetValue(EditModeProperty, value);
                OnEditModeChanged();
            }
        }

        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        public FieldType Type { get; init; }

        bool hasFocus = false;
        public bool HasFocus
        {
            get => hasFocus;
            set => SetFocusBorder(value);
        }

        public EDDTextBox()
        {
            InitializeComponent();
            Panel.SetZIndex(this, 0);

            textBox.IsReadOnlyCaretVisible = false;
            textBox.LostFocus += TextBox_LostFocus;

            _foreground = Foreground;
        }

        private void SetTooltip(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                ToolTip = null;
                return;
            }

            if (ToolTip is ToolTip tt)
            {
                tt.Content = content;
                return;
            }

            ToolTip = new ToolTip()
            {
                Content = content
            };
        }

        protected virtual void OnEditModeChanged()
        {
            switch (EditMode)
            {
                case EditMode.CanEdit:
                    textBox.IsReadOnly = false;
                    textBox.IsHitTestVisible = true;
                    textBox.Background = Brushes.White;
                    Foreground = SetForeground(null);
                    break;
                case EditMode.CanModify:
                    textBox.IsReadOnly = true;
                    textBox.IsHitTestVisible = false;
                    textBox.Background = Brushes.Transparent;
                    Foreground = SetForeground(null);
                    break;
                case EditMode.Locked:
                    textBox.IsReadOnly = true;
                    textBox.IsHitTestVisible = false;
                    textBox.Background = Brushes.Transparent;
                    Foreground = SetForeground(EDDBrushes.Gray);
                    break;
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            Panel.SetZIndex(this, int.MaxValue);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (!textBox.IsFocused)
                Panel.SetZIndex(this, 0);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Panel.SetZIndex(this, 0);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);

            App.SelectedTextBox = this;
        }

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not EDDTextBox textBox)
                return;

            switch (e.Property.Name)
            {
                case nameof(EditMode):
                    textBox.OnEditModeChanged();
                    break;
            }
        }

        private void SetFocusBorder(bool hasFocus)
        {
            if (EditMode == EditMode.CanEdit && hasFocus)
                focusBorder.Visibility = Visibility.Collapsed;
            else
                focusBorder.Visibility = hasFocus ? Visibility.Visible : Visibility.Collapsed;
        }


        private Brush SetForeground(Brush? brush)
        {
            if (brush is null)
                return _foreground;
            else
                return brush;
        }
    }
}
