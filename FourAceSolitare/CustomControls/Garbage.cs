using FourAceSolitaire.Helper;
using FourAceSolitaire.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FourAceSolitaire.CustomControls
{
    public class Garbage : Grid
    {
        public static Garbage GarbageInstance { get; private set; }


        public static readonly DependencyProperty RemovedCardsProperty =
                    DependencyProperty.Register("RemovedCards", typeof(ObservableCollection<CardModel>), typeof(Garbage),
                    new PropertyMetadata(new ObservableCollection<CardModel>(), PropertyChangedCallBack));


        public Rect GarbageRect { get => new Rect(new Point(Margin.Left, Margin.Top), new Size(ActualWidth, ActualHeight)); }

        public ObservableCollection<CardModel> RemovedCards
        {
            get { return (ObservableCollection<CardModel>)GetValue(RemovedCardsProperty); }
            set { SetValue(RemovedCardsProperty, value); }
        }



       
        private static void PropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                (e.NewValue as ObservableCollection<CardModel>).CollectionChanged += (s, ee) =>
                {
                    if (ee.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                    {
                        CardThumb cardThumb = new CardThumb();
                        cardThumb.DataContext = ee.NewItems[0] as CardModel;
                        cardThumb.IsEnabled = false;
                        cardThumb.Template = Application.Current.Resources["cardThumbTemplate"] as ControlTemplate;
                        cardThumb.IsDeleted = true;
                        cardThumb.HorizontalAlignment = HorizontalAlignment.Center;
                        cardThumb.VerticalAlignment = VerticalAlignment.Center;
                        (d as Garbage).Children.Add(cardThumb);
                    }
                    else if(ee.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset|
                            ee.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                    {
                        (d as Garbage).Children.Clear();
                    }
                };
            }
        }

        public Garbage()
        {
            GarbageInstance = this;
        }
    }
}
