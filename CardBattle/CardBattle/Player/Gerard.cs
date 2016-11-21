using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;
using CardBattle.Player;
using CardBattle.Infrastructure;

namespace CardBattle
{
    class Gerard : IPlayer
    {
        public string Author
        {
            get
            {
                return "Sexy Valentin" ;
            }
        }

        public string Name
        {
            get
            {
               return "Gerard le fermier qui joue aux cartes";
            }
        }

        public void Deal( IEnumerable<Card> cards )
        {
            foreach(Card card in cards)
            {
                myCards.Add( card );
            }

            Sort.Bubble( myCards );
        }

        public void Initialize( int playerCount , int position )
        {
            myPlayerCount = playerCount;
            myPosition = position;
            //CardDealer dealer = new CardDealer();
            //allCardsPlayed = dealer.Deal( 52 );
        }

        public Card PlayCard()
        {
            Card currentCard = endList ? myCards[myCards.Count-1] :myCards[0];
            myCards.Remove( currentCard );
            endList = !endList;

            return currentCard;
        }

        public void ReceiveFoldResult( FoldResult result )
        {
            foreach(Card card in result.CardsPlayed)
            {
                allCardsPlayed.Remove( card );
            }
        }

        private bool endList = true;
        private int myPosition;
        private int myPlayerCount;
        private List<Card> myCards = new List<Card>();
        private List<Card> allCardsPlayed = new List<Card>();
    }
}
