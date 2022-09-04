using Communication.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace EEDD.Form
{
    public abstract class Row : UserControl, INotifyPropertyChanged
    {
        Brush backColor;
        bool odd = false;
        StationColor color = StationColor.Gray;
        RowType rowType = RowType.Both;
        
        protected Brush BackColor { get => backColor; }

        public bool Odd
        {
            get => odd;
            set
            {
                bool flag = odd != value;
                odd = value;
                if (flag)
                    UpdateColor();
            }
        }

        public StationColor Color
        {
            get => color;
            init
            {
                color = value;
                UpdateColor();
            }
        }

        public RowType RowType
        {
            get => rowType;
            init
            {
                rowType = value;
                UpdateColor();
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public Row(StationColor color, RowType rowType)
        {
            Color = color;
            RowType = rowType;
        }

        private void UpdateColor()
        {
            if (rowType == RowType.Comment)
                color = StationColor.Gray;

            if (color == StationColor.Green)
            {
                if (odd)
                    backColor = EDDBrushes.BackgroundGreen1;
                else
                    backColor = EDDBrushes.BackgroundGreen2;
            }
            else if (color == StationColor.Yellow)
            {
                if (odd)
                    backColor = EDDBrushes.BackgroundYellow1;
                else
                    backColor = EDDBrushes.BackgroundYellow2;
            }
            else
            {
                if (odd)
                    backColor = EDDBrushes.BackgroundGray1;
                else
                    backColor = EDDBrushes.BackgroundGray2;
            }

            OnPropertyChanged(nameof(BackColor));
        }

        public abstract void Next(EDDTextBox prev, bool tab = false);

        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
