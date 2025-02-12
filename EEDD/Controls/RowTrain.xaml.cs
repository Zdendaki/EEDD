using Common.Data;
using System;

namespace EEDD.Controls
{
    /// <summary>
    /// Interakční logika pro Row.xaml
    /// </summary>
    public partial class RowTrain : Row
    {
        StationColor _color;
        bool _chained;

        public RowTrain(StationColor color, bool chained) : base()
        {
            InitializeComponent();

            _color = color;
            _chained = chained;

            if (_chained)
            {
                border.BorderThickness = new(0.5d, 0.5d, 0.5d, 0);
                Margin = new(0, 0, 0, -0.5d);
            }
        }

        [Obsolete("Only for designer", true)]
        public RowTrain() : this(StationColor.Gray, false) { }

        public override bool SetBackground(bool odd)
        {
            Background = RowHelper.GetBackground(_color, odd);
            return _chained;
        }
    }
}
