using System.Windows;
using System.Windows.Controls;

namespace EEDD.Controls
{
    public class RowsList : VirtualizingStackPanel
    {
        private readonly object _rearrangeLock = new();

        public RowsList() : base()
        {
            Orientation = Orientation.Vertical;
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);

            if (visualAdded is not Row)
                throw new InvalidOperationException("Only Row can be added to RowsList!");

            RearrangeRows();
        }

        private void RearrangeRows()
        {
            lock (_rearrangeLock)
            {
                bool odd = false;
                foreach (Row row in Children)
                {
                    if (!row.SetBackground(odd))
                        odd = !odd;
                }
            }
        }
    }
}
