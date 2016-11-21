using CardBattle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle.Infrastructure
{
    public class CardDealer
    {
        private static readonly int _suitsCount = Enum.GetValues(typeof(Suit)).Length;
        private static readonly int _valuesCount = Enum.GetValues(typeof(Values)).Length;

        private List<Card> _availableCards;
        private readonly Object _lock = new object();
        private readonly RandomProvider _randomProvider;

        private List<Card> AvailableCards
        {
            get
            {
                if (_availableCards == null)
                {
                    lock (this._lock)
                    {
                        if (_availableCards == null)
                        {
                            _availableCards = new List<Card>();
                        }
                    }
                }
                return _availableCards;
            }

            set
            {
                this._availableCards = value;
            }
        }

        public CardDealer(RandomProvider randomProvider)
        {
            _randomProvider = randomProvider;
            Shuffle();
        }

        public int RemainingCards
        {
            get
            {
                return AvailableCards.Count;
            }
        }



        public Card RandomCard()
        {
            if (AvailableCards.Count == 0)
            {
                throw new InvalidOperationException("Nocard available.");
            }

            var index = _randomProvider.Random.Next(AvailableCards.Count);

            var result = AvailableCards[index];

            AvailableCards.RemoveAt(index);

            return result;
        }

        public void Shuffle()
        {
            var list = new List<Card>();
            for (var i = 0; i < _suitsCount; i++)
            {
                for (var j = 0; j < _valuesCount; j++)
                {
                    list.Add(new Card((Values)j, (Suit)i));
                }
            }

            AvailableCards = list;
        }

        public List<Card> Deal(int n)
        {
            if (n > AvailableCards.Count)
            {
                throw new ArgumentException("Not enough cards available");
            }

            var result = new List<Card>();
            for (var i = 0; i < n; i++)
            {
                result.Add(RandomCard());
            }

            return result;
        }

        public IEnumerable<Card> Frob(IEnumerable<Card> input)
        {
            foreach(var card in input)
            {
                if (card.Suit == Suit.Diamonds || card.Suit == Suit.Hearts)
                {
                    yield return NextCard(card);
                }
            }
        }

        private Card NextCard(Card card)
        {
            return new Card((Values)((int)(card.Value + 1) % _valuesCount), card.Suit); 
        }

        public IEnumerable<Card> Frob2(IEnumerable<Card> input)
        {
            return input.Where(c => c.Suit == Suit.Diamonds || c.Suit == Suit.Hearts)
                .Select(NextCard);
        }
    }
}
