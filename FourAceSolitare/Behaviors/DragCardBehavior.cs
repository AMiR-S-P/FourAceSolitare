using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FourAceSolitaire.Behaviors
{
    internal class DragCardBehavior:Behavior<Thumb>
    {
        double Left;
        double Top;
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.DragStarted += AssociatedObject_DragStarted;
            AssociatedObject.DragDelta += AssociatedObject_DragDelta;
            AssociatedObject.DragCompleted += AssociatedObject_DragCompleted;
            AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, System.Windows.DragEventArgs e)
        {
         }

        private void AssociatedObject_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            Mouse.Capture(null);
            Panel.SetZIndex(AssociatedObject, -98);

        }

        private void AssociatedObject_DragDelta(object sender, DragDeltaEventArgs e)
        {
            AssociatedObject.Margin = new System.Windows.Thickness(AssociatedObject.Margin.Left + e.HorizontalChange,
                                                                   AssociatedObject.Margin.Top + e.VerticalChange,
                                                                  - AssociatedObject.Margin.Left - e.HorizontalChange,
                                                                   -AssociatedObject.Margin.Top - e.VerticalChange);
        }

        private void AssociatedObject_DragStarted(object sender, DragStartedEventArgs e)
        {
            Mouse.Capture(AssociatedObject);
            Left = (sender as Thumb).Margin.Left;
            Top = (sender as Thumb).Margin.Top;
            Panel.SetZIndex(AssociatedObject, 10);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
