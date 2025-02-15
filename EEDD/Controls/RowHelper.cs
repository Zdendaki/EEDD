using Common.Data;
using System.Windows.Media;

namespace EEDD.Controls
{
    internal static class RowHelper
    {
        public static Brush GetBackground(StationColor color, bool odd)
        {
            switch (color)
            {
                case StationColor.Gray:
                    return odd ? EDDBrushes.BackgroundGray1 : EDDBrushes.BackgroundGray2;
                case StationColor.Green:
                    return odd ? EDDBrushes.BackgroundGreen1 : EDDBrushes.BackgroundGreen2;
                case StationColor.Yellow:
                    return odd ? EDDBrushes.BackgroundYellow1 : EDDBrushes.BackgroundYellow2;
                default:
                    throw new InvalidOperationException("Unknown color");
            }
        }
    }
}
