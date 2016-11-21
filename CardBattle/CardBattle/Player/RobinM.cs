using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;
using CardBattle.Player;
using CardBattle.Infrastructure;

namespace CardBattle.GameBase
{
    public class RobinM : IPlayer
    {
        int numberOfPlayer;
        int positionIngame;
        List<Card> sortedCards;
        List<Card> cards;
        List<FoldResult> turns;
        List<Card> played;
        int count;
        const int MAXCOUNT = 20;
        RandomProvider random;

        public string Name
        {
            get
            {
                return "Card Bot";
            }
        }

        public string Author
        {
            get
            {
                return "Robin Mathieu";
            }
        }

        public void Initialize(int playerCount, int position)
        {
            numberOfPlayer = playerCount;
            positionIngame = position;
        }

        public void Deal(IEnumerable<Card> cards)
        {
            sortedCards = new List<Card>(cards);
            sortedCards.Sort();
            this.cards = new List<Card>(cards);
            played = new List<Card>();
            turns = new List<FoldResult>();
            count = 0;
            random = new RandomProvider();
        }

        public Card PlayCard()
        {
            if (sortedCards.Count == 0)
            {
                throw new InvalidOperationException("There is no card left");
            }
            if (sortedCards.Count == 1)
            {
                return sortedCards[0];
            }
            var index = sortedCards.Count / 2 - 1;
            if (cards.Count * numberOfPlayer < MAXCOUNT * 2)
            {
                index = random.Random.Next(0, sortedCards.Count);
            }
            else if (played.Count > 0)
            {
                index = index + (int)Math.Floor(index * (float)(count / MAXCOUNT));
            }
            Card card = sortedCards[index];
            sortedCards.Remove(card);
            return card;
        }

        public void ReceiveFoldResult(FoldResult cardsPlayed)
        {
            turns.Add(cardsPlayed);
            played.AddRange(cardsPlayed.CardsPlayed);
            CountCard(cardsPlayed.CardsPlayed);
        }

        void CountCard(IEnumerable<Card> cards)
        {
            foreach (Card card in cards)
            {
                count += card.Value <= Values.Six ? 1 : card.Value >= Values.Ten ? -1 : 0;
            }
        }
    }
}
