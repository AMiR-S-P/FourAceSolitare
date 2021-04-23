using FourAceSolitaire.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace FourAceSolitaire.CustomControls
{
    public class Table : Grid
    {
        static Table instance;
        public static Table Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Table();
                }
                return instance;
            }
        }

        public CardsColumn C1 { get; set; } = new CardsColumn();
        public CardsColumn C2 { get; set; }  = new CardsColumn();
        public CardsColumn C3 { get; set; }  = new CardsColumn();
        public CardsColumn C4 { get; set; } = new CardsColumn();

        public Garbage Garbage { get; set; } = new Garbage();

        public Table()
        {
            instance = this;
            DataContextChanged += Table_DataContextChanged;

        }

        private void Table_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue!=null)
            {
                Init();
            }
        }

        void Init()
        {
            C1.Margin = new System.Windows.Thickness(0, 0, 0, 0);
            C1.Style = Application.Current.Resources["cardsColumn"] as Style;
            C1.SetBinding(CardsColumn.ItemsSourceProperty, new Binding("Column1") { Source = DataContext as MainWindowVM });
            C1.HorizontalAlignment = HorizontalAlignment.Left;
            C1.Width = 152;
            C1.Style = Application.Current.Resources["cardsColumnStyle"] as Style;
            BehaviorCollection behaviorsC1 = Interaction.GetBehaviors(C1);
            behaviorsC1.Add(new Behaviors.DisablePreviewsCardBehavior());
            Panel.SetZIndex(C1, -99);

            C2.Margin = new System.Windows.Thickness(152, 0, 0, 0);
            C2.Style = Application.Current.Resources["cardsColumn"] as Style;
            C2.SetBinding(CardsColumn.ItemsSourceProperty, new Binding("Column2") { Source = DataContext as MainWindowVM });
            C2.HorizontalAlignment = HorizontalAlignment.Left;
            C2.Width = 152;
            C2.Style = Application.Current.Resources["cardsColumnStyle"] as Style;
            BehaviorCollection behaviorsC2 = Interaction.GetBehaviors(C2);
            behaviorsC2.Add(new Behaviors.DisablePreviewsCardBehavior());
            Panel.SetZIndex(C2, -99);

            C3.Margin = new System.Windows.Thickness(304, 0, 0, 0);
            C3.Style = Application.Current.Resources["cardsColumn"] as Style;
            C3.SetBinding(CardsColumn.ItemsSourceProperty, new Binding("Column3") { Source = DataContext as MainWindowVM });
            C3.HorizontalAlignment = HorizontalAlignment.Left;
            C3.Width = 152;
            C3.Style = Application.Current.Resources["cardsColumnStyle"] as Style;
            BehaviorCollection behaviorsC3 = Interaction.GetBehaviors(C3);
            behaviorsC3.Add(new Behaviors.DisablePreviewsCardBehavior());
            Panel.SetZIndex(C3, -99);

            C4.Margin = new System.Windows.Thickness(456, 0, 0, 0);
            C4.Style = Application.Current.Resources["cardsColumn"] as Style;
            C4.SetBinding(CardsColumn.ItemsSourceProperty, new Binding("Column4") { Source = DataContext as MainWindowVM });
            C4.HorizontalAlignment = HorizontalAlignment.Left;
            C4.Width = 152;
            C4.Style = Application.Current.Resources["cardsColumnStyle"] as Style;
            BehaviorCollection behaviorsC4 = Interaction.GetBehaviors(C4);
            behaviorsC4.Add(new Behaviors.DisablePreviewsCardBehavior());
            Panel.SetZIndex(C4, -99);

            Garbage.Margin = new Thickness(680, 50, 20, 0);
            Garbage.SetBinding(Garbage.RemovedCardsProperty, new Binding("DeletedCards") { Source = DataContext as MainWindowVM });
            Garbage.Width = Garbage.Height = 250;
            Garbage.HorizontalAlignment = HorizontalAlignment.Center;
            Garbage.VerticalAlignment = VerticalAlignment.Top;
            Garbage.Effect = new DropShadowEffect() { BlurRadius = 30, ShadowDepth = 5, Direction = 270, Color = Colors.Red };
            Garbage.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/RecycleBin.png", UriKind.Absolute))) { Viewbox = new Rect(-.25, -.25, 1.5, 1.5) };
            Panel.SetZIndex(Garbage, -999);

            this.Children.Add(C1);
            this.Children.Add(C2);
            this.Children.Add(C3);
            this.Children.Add(C4);
            this.Children.Add(Garbage);
        }

    }
}
