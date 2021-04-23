using FourAceSolitaire.Helper;
using FourAceSolitaire.Model;
using FourAceSolitaire.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace FourAceSolitaire.CustomControls
{
    public class CardThumb : Thumb
    {


        public ItemsControl Column
        {
            get { return (ItemsControl)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Column.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.Register("Column", typeof(ItemsControl), typeof(CardThumb), new PropertyMetadata(null));


        public bool IsDeleted
        {
            get { return (bool)GetValue(IsDeletedProperty); }
            set { SetValue(IsDeletedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDeleted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDeletedProperty =
            DependencyProperty.Register("IsDeleted", typeof(bool), typeof(CardThumb), new PropertyMetadata(false));



        public bool IsDeleting
        {
            get { return (bool)GetValue(IsDeletingProperty); }
            set { SetValue(IsDeletingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDeleting.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDeletingProperty =
            DependencyProperty.Register("IsDeleting", typeof(bool), typeof(CardThumb), new PropertyMetadata(false));




        public bool IsMoving
        {
            get { return (bool)GetValue(IsMovingProperty); }
            set { SetValue(IsMovingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMoving.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMovingProperty =
            DependencyProperty.Register("IsMoving", typeof(bool), typeof(CardThumb), new PropertyMetadata(false));



        //because thumb is in cardgrid it's margin always is 0.
        //bind Margin.Left of itemscontrol to this property
        public double Left
        {
            get;
            set;
        }

        //top is always zero.position index should multiply with 40.
        public double Top
        {
            get;
            set;
        }


        double LastLeft;
        double LastTop;
        bool movingToC1;
        bool movingToC2;
        bool movingToC3;
        bool movingToC4;

        public CardThumb()
        {
            Width = 150;
            Height = 180;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF059761"));
            Margin = new Thickness(0);

            DragStarted += CardThumb_DragStarted;
            DragDelta += CardThumb_DragDelta;
            DragCompleted += CardThumb_DragCompleted;
        }





        private async void CardThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            Mouse.Capture(null);

            if (IsDeleting)
            {
                //move to garbage
                IsDeleted = await (Column.DataContext as MainWindowVM).SendToGarbage((Column.ItemsSource as ObservableCollection<CardModel>));

                if (IsDeleted == false)
                {
                    Margin = new Thickness(LastLeft, LastTop, 0, 0);
                    IsDeleting = false;
                }
            }
            else
            {
                Margin = new Thickness(LastLeft, LastTop, 0, 0);

            }

            if (movingToC1 && Table.Instance.C1.Items.Count == 0)
            {
                await (Column.DataContext as MainWindowVM).MoveCard((Column.ItemsSource as ObservableCollection<CardModel>), (Column.DataContext as MainWindowVM).Column1);
            }
            else if (movingToC2 && Table.Instance.C2.Items.Count == 0)
            {
                await (Column.DataContext as MainWindowVM).MoveCard((Column.ItemsSource as ObservableCollection<CardModel>), (Column.DataContext as MainWindowVM).Column2);
            }
            else if (movingToC3 && Table.Instance.C3.Items.Count == 0)
            {
                await (Column.DataContext as MainWindowVM).MoveCard((Column.ItemsSource as ObservableCollection<CardModel>), (Column.DataContext as MainWindowVM).Column3);
            }
            else if (movingToC4 && Table.Instance.C4.Items.Count == 0)
            {
                await (Column.DataContext as MainWindowVM).MoveCard((Column.ItemsSource as ObservableCollection<CardModel>), (Column.DataContext as MainWindowVM).Column4);
            }

            IsMoving = false;
            Table.Instance.C1.IsReceiveingCard = false;
            Table.Instance.C2.IsReceiveingCard = false;
            Table.Instance.C3.IsReceiveingCard = false;
            Table.Instance.C4.IsReceiveingCard = false;
        }

        private void CardThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Margin = new System.Windows.Thickness(Margin.Left + e.HorizontalChange,
                                                  Margin.Top + e.VerticalChange,
                                                  -Margin.Left - e.HorizontalChange,
                                                  -Margin.Top - e.VerticalChange);


            var cRect = new Rect(Margin.Left + Left, Margin.Top + Top, ActualWidth, ActualHeight);

            var deleting = cRect.IntersectsWith(Garbage.GarbageInstance.GarbageRect);
            if (deleting)
            {
                IsDeleting = true;
            }
            else
            {
                IsDeleting = false;
            }


            movingToC1 = cRect.IntersectsWith(Table.Instance.C1.CardsColumnRect);
            movingToC2 = cRect.IntersectsWith(Table.Instance.C2.CardsColumnRect);
            movingToC3 = cRect.IntersectsWith(Table.Instance.C3.CardsColumnRect);
            movingToC4 = cRect.IntersectsWith(Table.Instance.C4.CardsColumnRect);
            if (movingToC1 || movingToC2 || movingToC3 || movingToC4)
            {
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }

            Table.Instance.C1.IsReceiveingCard = movingToC1 && Table.Instance.C1.Items.Count == 0;
            Table.Instance.C2.IsReceiveingCard = movingToC2 && Table.Instance.C2.Items.Count == 0;
            Table.Instance.C3.IsReceiveingCard = movingToC3 && Table.Instance.C3.Items.Count == 0;
            Table.Instance.C4.IsReceiveingCard = movingToC4 && Table.Instance.C4.Items.Count == 0;


        }

        private void CardThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            Top = (Column.ItemsSource as ObservableCollection<CardModel>).IndexOf(this.DataContext as CardModel) * 40;
            Left = Column.Margin.Left;


            IsDeleting = true;
            Mouse.Capture(this);
            LastLeft = (sender as Thumb).Margin.Left;
            LastTop = (sender as Thumb).Margin.Top;

        }


    }
}
