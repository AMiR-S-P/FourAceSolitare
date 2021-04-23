using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourAceSolitaire.Model
{
    public class CardModel : BaseModel, IComparable<CardModel>
    {
        public enum CardType { Heart, Diamond, Club, Spade }
        public enum CardName { Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14, };



        CardType cType = new CardType();
        CardName cName = new CardName();

        public CardType CType
        {
            set
            {
                cType = value;
                OnPropertyChanged(nameof(CType));
            }
            get
            {
                return cType;
            }
        }
        public CardName CName
        {
            set
            {
                cName = value;
                OnPropertyChanged(nameof(CName));
            }
            get
            {
                return cName;
            }
        }

        public Uri NumberUri
        {
            get
            {
                switch (cType)
                {
                    case CardType.Club:
                    case CardType.Spade:
                        return new Uri("pack://application:,,,/Resources/Cards/" + CName + "Black.png");
                    case CardType.Heart:
                    case CardType.Diamond:
                        return new Uri("pack://application:,,,/Resources/Cards/" + CName + "Red.png");
                }

                return null;
            }
        }
        public Uri TypeUri
        {
            get
            {
                switch (CType)
                {
                    case CardModel.CardType.Club:
                        return new Uri("pack://application:,,,/Resources/Cards/Club.png");

                    case CardModel.CardType.Spade:
                        return new Uri("pack://application:,,,/Resources/Cards/Spade.png");

                    case CardModel.CardType.Heart:
                        return new Uri("pack://application:,,,/Resources/Cards/Heart.png");

                    case CardModel.CardType.Diamond:
                        return new Uri("pack://application:,,,/Resources/Cards/Diamond.png");
                }
                return null;
            }
        }
        public CardName GetCardName(String name)
        {
            foreach (var n in Enum.GetValues(typeof(CardName)))
            {
                if (n.ToString() == name.ToString())
                {
                    return (CardName)n;
                }
            }
            throw new ArgumentOutOfRangeException();
        }
        public CardType GetCardType(String name)
        {
            foreach (var n in Enum.GetValues(typeof(CardType)))
            {
                if (n.ToString() == name.ToString())
                {
                    return (CardType)n;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        public override string ToString()
        {
            return this.CName.ToString() + " " + this.CType.ToString();
        }


        public int CompareTo(CardModel other)
        {
            if (other == null)
            {
                return 0;
            }
            if (this.CType == (other).CType)
            {
                if (this.CName > (other).CName)
                {
                    return 1;//Greater
                }
                else if (this.CName < (other).CName)
                {
                    return -1;//Lower
                }
                else
                {
                    return 0;//Equal
                }

            }
            return 0;//Type mismatch        
        }

        public static bool operator >(CardModel m1, CardModel m2)
        {
            return m1.CompareTo(m2) > 0;
        }
        public static bool operator <(CardModel m1, CardModel m2)
        {
            return m1.CompareTo(m2) < 0;
        }
    }
}
