using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;
using CardBattle.Player;
using CardBattle.Infrastructure;

namespace CardBattle
{
    public class Gertrude : IPlayer
    {

        IEnumerable<Card> myCards;

        int nbPlayer;
        int myPosition;
        Card middleCard;
        int startSecondStratTurn;
        int turnIndex = 0;
        IEnumerable<Card> AllCards;

        public string Author
        {
            get
            {
                return "Benjamin";
            }
        }

        public string Name
        {
            get
            {
                return "Gertrude";
            }
        }

        public void Deal(IEnumerable<Card> cards)
        {
            myCards = cards;
            myCards.ToList().Sort();
            startSecondStratTurn = (int)((myCards.ToList().Count / (float)nbPlayer) * 0.2f);
        }

        public void Initialize(int playerCount, int position)
        {
            nbPlayer = playerCount;
            myPosition = position;
            //CardDealer cd = new CardDealer();
            //AllCards = cd.Deal(52);
            //AllCards.ToList().Sort();
        }

        public Card PlayCard()
        {
            List<Card> toList = myCards.ToList();
            Card ret;
            if(turnIndex < startSecondStratTurn)
            {
                // return from min to max
                ret = toList.ElementAt(0);
                toList.RemoveAt(0);
            }
            else
            {
                /*ret = toList.Last();
                toList.Remove(ret);*/
                
                // just better than middle
                ret = myCards.First();
                foreach(Card c in myCards)
                {
                    ret = c;
                    if (c.CompareTo(middleCard) > 0)
                        break;
              }
            }
            return ret;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            foreach(Card c in result.CardsPlayed)
            {
                AllCards.ToList().Remove(c);
            }
            middleCard = AllCards.ToList().ElementAt(AllCards.ToList().Count / 2);
            turnIndex++;
        }
    }
}
