using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FourAceSolitaire.CustomControls
{
    public class CardsColumn : ItemsControl
    {
        public static readonly DependencyProperty IsReceiveingCardProperty =
       DependencyProperty.Register("IsReceiveingCard", typeof(bool), typeof(CardsColumn), new PropertyMetadata(false));

        static CardsColumn instance;
        public static CardsColumn Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CardsColumn();
                }
                return instance;
            }
        }



        public bool IsReceiveingCard
        {
            get { return (bool)GetValue(IsReceiveingCardProperty); }
            set { SetValue(IsReceiveingCardProperty, value); }
        }




        public Rect CardsColumnRect { get { return new Rect( Margin.Left, Margin.Top, ActualWidth, ActualHeight); } }

        public CardsColumn()
        {
            FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(CardGrid));
            ItemsPanel = new ItemsPanelTemplate(frameworkElementFactory);

            MouseEnter += CardsColumn_MouseEnter;
            MouseLeave += CardsColumn_MouseLeave;
        }

        private void CardsColumn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Panel.SetZIndex(this, -99);
        }

        private void CardsColumn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Panel.SetZIndex(this, 0);
        }
    }
}
