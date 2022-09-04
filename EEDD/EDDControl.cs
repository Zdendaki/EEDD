using EEDD.Form;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EEDD
{
    public abstract class EDDControl : UserControl, INotifyPropertyChanged
    {
        string text;
        string caption;
        bool longText;
        HorizontalAlignment align;
        Brush background = EDDBrushes.Transparent;
        Brush foreground = EDDBrushes.Black;
        Brush innerBorder = EDDBrushes.Transparent;

        public abstract bool HasFocus { get; set; }

        public Row? Row { get; set; }

        public string Text
        {
            get => text;
            set
            {
                bool flag = text != value;
                text = value;
                if (flag)
                    OnPropertyChanged(nameof(Text));
            }
        }

        public string Caption
        {
            get
            {
                return longText ? text : caption;
            }
            set
            {
                bool flag = caption != value;
                caption = value;
                if (flag)
                    OnPropertyChanged(nameof(Caption));
            }
        }

        public int MaxLength { get; init; } = int.MaxValue;

        public bool LongText
        {
            get => longText;
            set
            {
                bool flag = longText != value;
                longText = value;
                if (flag)
                    OnPropertyChanged(nameof(Caption));
            }
        }

        public HorizontalAlignment Align
        {
            get => align;
            set
            {
                bool flag = align != value;
                align = value;
                if (flag)
                    OnPropertyChanged(nameof(Align));
            }
        }

        public Brush ForeColor
        {
            get => foreground;
            set
            {
                bool flag = foreground != value;
                foreground = value;
                if (flag)
                    OnPropertyChanged(nameof(ForeColor));
            }
        }

        public Brush BackColor
        {
            get => background;
            set
            {
                bool flag = background != value;
                background = value;
                if (flag)
                    OnPropertyChanged(nameof(BackColor));
            }
        }

        public Brush InnerBorder
        {
            get => innerBorder;
            set
            {
                bool flag = innerBorder != value;
                innerBorder = value;
                if (flag)
                    OnPropertyChanged(nameof(InnerBorder));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public EDDControl()
        {
            
        }

        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public enum EditMode
    {
        CanEdit,
        CanModify,
        Locked
    }

    public enum FieldType
    {
        Static,
        Number,
        String,
        Time
    }
}
