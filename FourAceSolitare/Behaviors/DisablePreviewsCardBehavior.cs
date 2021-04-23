using FourAceSolitaire.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace FourAceSolitaire.Behaviors
{
    internal class DisablePreviewsCardBehavior:Behavior<ItemsControl>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Initialized += AssociatedObject_Initialized;
        }

        private void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            if (AssociatedObject.ItemsSource is ObservableCollection<CardModel>)
            {
                (AssociatedObject.ItemsSource as ObservableCollection<CardModel>).CollectionChanged += DisablePreviewsCardBehavior_CollectionChanged;
            }
        }

        private void DisablePreviewsCardBehavior_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if ((AssociatedObject.ItemsSource as ObservableCollection<CardModel>).Count > 1)
                {
                    var itemUi = AssociatedObject.ItemContainerGenerator.ContainerFromIndex((AssociatedObject.Items).Count - 2) as UIElement;
                    itemUi.IsEnabled = false;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                if ((AssociatedObject.ItemsSource as ObservableCollection<CardModel>).Count > 0)
                {
                    var itemUi = AssociatedObject.ItemContainerGenerator.ContainerFromIndex((AssociatedObject.Items).Count - 1) as UIElement;
                    itemUi.IsEnabled = true;
                }
            }
        }
    }
}
