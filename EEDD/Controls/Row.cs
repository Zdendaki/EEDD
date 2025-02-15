using Common.Data;
using System.Windows.Controls;
using System.Windows.Input;

namespace EEDD.Controls
{
    public abstract class Row : UserControl
    {
        public Row()
        {
            Panel.SetZIndex(this, 0);

            Loaded += (_, _) => Init();
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            Panel.SetZIndex(this, int.MaxValue);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            Panel.SetZIndex(this, 0);
        }

        /// <summary>
        /// Set background color of row
        /// </summary>
        /// <param name="odd">Row is odd</param>
        /// <returns><see langword="true"/> if next row is chained</returns>
        public virtual bool SetBackground(bool odd)
        {
            Background = RowHelper.GetBackground(StationColor.Gray, odd);
            return false;
        }

        protected abstract void Init();
    }
}
