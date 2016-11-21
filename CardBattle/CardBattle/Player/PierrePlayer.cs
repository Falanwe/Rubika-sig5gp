using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;
using System.IO;
using CardBattle.Player;

namespace CardBattle
{
    class PierrePlayer : IPlayer
    {
        public string Name
        {
            get
            {
                return "Robert Tomp";
            }
        }

        public string Author
        {
            get
            {
                return "Pierre";
            }
        }

        private int playerCount;
        private int position;

        private List<Card> myHand;
        private List<Card> usedCard;

        private int nbCard;
        private int nbVictory;
        private int nbGame;

        private bool playBestCards;

        public void Initialize(int playerCount, int position)
        {
            this.playerCount = playerCount;
            this.position = position;

            nbCard = 0;
            nbGame = 0;
            nbVictory = 0;
            
            playBestCards = false;

            myHand = new List<Card>();
            usedCard = new List<Card>();
        }

        public void Deal(IEnumerable<Card> cards)
        {
            foreach(Card card in cards)
            {
                myHand.Add(card);
            }

            myHand = Sort.QuickSort(myHand);

            nbCard = cards.Count();

            nbCard = 0;
            nbGame = 0;

            playBestCards = false;
            nbVictory = 0;

            usedCard.Clear();
        }

        public Card PlayCard()
        {
            Card myCard = playBestCards ? myHand.Last() : myHand.First();
            
            myHand.Remove(myCard);

            return myCard;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            foreach(Card card in result.CardsPlayed)
            {
                usedCard.Add(card);
            }

            nbGame++;

            if(result.Winner == position)
            {
                nbVictory++;
            }

            if(nbGame >= 2/* && nbVictory == 0*/)
            {
                playBestCards = true;
            }
        }
    }
}
