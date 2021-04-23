using FourAceSolitaire.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FourAceSolitaire.CustomControls
{
    class CardGrid:Grid
    {
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (visualAdded != null)
            {
                (visualAdded as FrameworkElement).Margin = new Thickness(0, 40 * (Children.Count - 1), 0, 0);
            }
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }
        
    }
}
