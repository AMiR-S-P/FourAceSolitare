using FourAceSolitaire.Command;
using FourAceSolitaire.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FourAceSolitaire.ViewModel
{
    class MainWindowVM : BaseViewModel
    {
        private int drawCount;
        private Stopwatch Stopwatch = new Stopwatch();
        private TimeSpan elapsedTime;
        Timer Timer = new Timer(10);
        private string message;

        public ObservableCollection<CardModel> Column1 { get; set; } = new ObservableCollection<CardModel>();
        public ObservableCollection<CardModel> Column2 { get; set; } = new ObservableCollection<CardModel>();
        public ObservableCollection<CardModel> Column3 { get; set; } = new ObservableCollection<CardModel>();
        public ObservableCollection<CardModel> Column4 { get; set; } = new ObservableCollection<CardModel>();

        public Stack<CardModel> Cards { get; set; }
        public ObservableCollection<CardModel> DeletedCards { get; set; } = new ObservableCollection<CardModel>();
        public TimeSpan ElapsedTime { get => elapsedTime; set { elapsedTime = value; OnPropertyChanged(); } }
        public int DrawCount { get => drawCount; set { drawCount = value; OnPropertyChanged(); } }
        public string Message { get => message; set { message = value; OnPropertyChanged(); } }
        public AsyncRelayCommand<object> DrawCommand { get; set; }
        public AsyncRelayCommand<object> NewGameCommand { get; set; }

        public MainWindowVM()
        {
            DrawCommand = new AsyncRelayCommand<object>(OnDraw, CanDraw);
            NewGameCommand = new AsyncRelayCommand<object>(OnNewGame);

            Timer.Elapsed += Timer_Elapsed;

            ElapsedTime = Stopwatch.Elapsed;

            Task.Run(async () =>
            {
                await OnNewGame(null);
            });
        }

        
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ElapsedTime = Stopwatch.Elapsed;
        }

        private async Task OnNewGame(object arg)
        {
            Message = "";
            Stopwatch.Reset();
            Timer.Enabled = false;

            await Shuffle();
        }

        private async Task OnDraw(object arg)
        {
            await Draw();
            if (drawCount == 0)
            {
                Timer.Enabled = true;
                Stopwatch.Start();
            }
            DrawCount++;
            if (drawCount == 13)
            {
                await Check();
            }
        }

        private bool CanDraw(object arg)
        {
            return Cards.Any();
        }


        public Task Check()
        {
            if (drawCount == 13)
            {
                if (Column1.LastOrDefault()?.CName == CardModel.CardName.Ace &&
                   Column2.LastOrDefault()?.CName == CardModel.CardName.Ace &&
                   Column3.LastOrDefault()?.CName == CardModel.CardName.Ace &&
                   Column4.LastOrDefault()?.CName == CardModel.CardName.Ace)
                {
                    if (Column1.Count + Column2.Count + Column3.Count + Column4.Count == 4)
                    {
                        Message = "You Win!";//win
                    }
                    else
                    {
                        Message = "x_-";
                    }
                }
                else
                {
                    Message = "Better luck next time!";
                }
            }
            else
            {
                if (Column1.LastOrDefault()?.CName == CardModel.CardName.Ace &&
                Column2.LastOrDefault()?.CName == CardModel.CardName.Ace &&
                Column3.LastOrDefault()?.CName == CardModel.CardName.Ace &&
                Column4.LastOrDefault()?.CName == CardModel.CardName.Ace)
                {
                    Message = "You are about to win.";
                }
            }

            return Task.CompletedTask;
        }

        public Task Shuffle()
        {
            DrawCount = 0;
            Column1.Clear();
            Column2.Clear();
            Column3.Clear();
            Column4.Clear();
            DeletedCards.Clear();


            List<CardModel> tempCard = new List<CardModel>(52);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    tempCard.Add(new CardModel() { CName = (CardModel.CardName)j, CType = (CardModel.CardType)i });
                }
            }

            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Cards = new Stack<CardModel>(tempCard.OrderBy(x => rnd.Next()).ToArray());

            return Task.CompletedTask;
        }



        public  Task Draw()
        {
            if (Cards.Count > 3)
            {
                Column1.Add(Cards.Pop());
                Column2.Add(Cards.Pop());
                Column3.Add(Cards.Pop());
                Column4.Add(Cards.Pop());
                DrawCommand.OnCanExecuteChanged();
            }
            return Task.CompletedTask;
        }

        public async Task<bool> SendToGarbage(ObservableCollection<CardModel> column)
        {
            bool canRemove = false;
            CardModel card = column.LastOrDefault();

            if (Column1.Any() && card < Column1.LastOrDefault())
            {
                canRemove = true;
            }


            else if (Column2.Any() && card < Column2.LastOrDefault())
            {
                canRemove = true;
            }


            else if (Column3.Any() && card < Column3.LastOrDefault())
            {
                canRemove = true;
            }

            else if (Column4.Any() && card < Column4.LastOrDefault())
            {
                canRemove = true;
            }


            if (canRemove)
            {
                column.Remove(card);
                DeletedCards.Add(card);
                return true;
            }

            await Check();
            return false;
        }

        public async Task<bool> MoveCard(ObservableCollection<CardModel> from, ObservableCollection<CardModel> to)
        {
            if (Column1.Any() &&
               Column2.Any() &&
               Column3.Any() &&
               Column4.Any())
            {
                return false;
            }

            if (to.Any())
            {
                return false;
            }

            CardModel fromCard = from.LastOrDefault();
            from.Remove(fromCard);
            to.Add(fromCard);

            await Check();
            return true;
        }
   
    

    }
}
