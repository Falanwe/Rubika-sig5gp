using System;
using System.Collections.Generic;
using CardBattle.Models;
using CardBattle.Player;

namespace CardBattle
{
    class PoulpePlayer : IPlayer
    {
        Random rand;
        string name = "Poulpe";
        string author = "BenoîtD";
        int place;
        int nbPlayer;
        int moy;
        int index;
        List<Card> hand;
        List<Card> cardsPlayed;
        List<Card> betterThanMine;
        List<Card> allCards;
        private static readonly int _suitsCount = Enum.GetValues(typeof(Suit)).Length;
        private static readonly int _valuesCount = Enum.GetValues(typeof(Values)).Length;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Author
        {
            get
            {
                return author;
            }
        }

        public void Initialize(int playerCount, int position)
        {
            allCards = new List<Card>();
            place = position;
            nbPlayer = playerCount;
            allCards = getAllCards();
        }

        public void Deal(IEnumerable<Card> cards)
        {
            hand = new List<Card>();
            cardsPlayed = new List<Card>();
            betterThanMine = new List<Card>();
            hand.AddRange(cards);
            hand = Sort.QuickSort(hand);
            betterThanMine = BetterCards(allCards, hand[hand.Count - 1]);
            rand = new Random();
            moy = hand.Count / 2;
            index = 0;
        }

        public Card PlayCard()
        {
            Card myCard = IA();
            RemoveCard(myCard);
            return myCard;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            cardsPlayed.AddRange(result.CardsPlayed);
        }

        Card IA()
        {
            if (moy < 10)
            {
                index++;
                if (index < moy)
                {
                    return hand[moy];
                }
                else
                {
                    return hand[hand.Count - 1];
                }
            }
            //return hand[rand.Next(0, hand.Count)];
            Card myCard = null;
            if (cardsPlayed.Count != 0 || betterThanMine.Count == 0)
            {
                cardsPlayed = Sort.QuickSort(cardsPlayed);
                if (betterThanMine.Count == 0 || ContainAll(betterThanMine, cardsPlayed))
                {
                    myCard = hand[hand.Count - 1];
                    if(hand.Count > 1)
                        betterThanMine = BetterCards(allCards, hand[hand.Count - 2]);
                }
                else
                {
                    myCard = hand[0];
                }
            }
            else
            {
                myCard = hand[0];
            }
            return myCard;
        }

        void RemoveCard(Card card)
        {
            hand.Remove(card);
        }

        bool ContainAll(List<Card> test, List<Card> check)
        {
            foreach (Card c in test)
            {
                if (!check.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

        List<Card> BetterCards(List<Card> cards, Card test)
        {
            List<Card> result = new List<Card>();
            for (int i = 0; i < allCards.Count; i++)
            {
                if (cards[i].CompareTo(test) == 1)
                {
                    result.Add(allCards[i]);
                }
            }
            return result;
        }

        List<Card> getAllCards()
        {
            var list = new List<Card>();
            for (var i = 0; i < _suitsCount; i++)
            {
                for (var j = 0; j < _valuesCount; j++)
                {
                    list.Add(new Card((Values)j, (Suit)i));
                }
            }
            list = Sort.QuickSort(list);
            return list;
        }
    }
}
