using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;
using CardBattle.Infrastructure;

namespace CardBattle.Player
{
    class Sarkhan : IPlayer
    {
        private string realName;

        public string Author
        {
            get
            {
                return "Jordan";
            }
        }

        public string Name
        {
            get
            {
                return realName;
            }
        }

        private int numberOfPlayer, myId;
        private List<Card> hand;
        private List<Card> cardPlayed;
        private enum playStyle{ C, D, R, RH, RM, RL };
        private playStyle ps;
        private RandomProvider r;



        public void Deal(IEnumerable<Card> cards)
        {
            hand = new List<Card>(cards);
            choosePlayStyle();
        }

        public void Initialize(int playerCount, int position)
        {
            numberOfPlayer = playerCount;
            myId = position;
            cardPlayed = new List<Card>();
        }

        public Card PlayCard()
        {
            Card myCard = null;

            if(ps == playStyle.C && hand.Count > 0)
            {
                myCard = hand[0];
                hand.RemoveAt(0);
            }

            if (ps == playStyle.D && hand.Count > 0)
            {
                myCard = hand[hand.Count-1];
                hand.RemoveAt(hand.Count-1);
            }

            if((ps == playStyle.R || ps == playStyle.RH || ps == playStyle.RM || ps == playStyle.RL) && hand.Count > 0)
            {
                int maxRange = hand.Count, minRange = 0;
                float tempCalc;

                if( ps == playStyle.RH)
                {
                    maxRange = hand.Count;

                    tempCalc = hand.Count / 4.0f;

                    if (tempCalc < 1)
                        tempCalc = 1;

                    minRange = hand.Count - (int) tempCalc;
                }

                if (ps == playStyle.RM)
                {
                    tempCalc = 3 * hand.Count / 8;

                    if (tempCalc < 1)
                        tempCalc = 0;

                    maxRange = hand.Count - (int) tempCalc;
                    minRange = (int) tempCalc;
                }

                if (ps == playStyle.RL)
                {
                    tempCalc = hand.Count / 4.0f;

                    if (tempCalc < 1)
                        tempCalc = 1;

                    maxRange = (int) tempCalc;
                    minRange = 0;
                }

                int indexMyCard = r.Next(minRange,maxRange);
                myCard = hand[indexMyCard];
                hand.RemoveAt(indexMyCard);
            }

            return myCard;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            cardPlayed.AddRange(result.CardsPlayed);
            cardPlayed.Sort();
        }

        private void choosePlayStyle()
        {
            hand.Sort();
            ps = playStyle.R;
            r = RandomProvider.SingleInstance();
            realName = "Sarkhan Le Fou";

            bool foundFirstNine = false;
            int i = 0;

            while(!foundFirstNine && i < hand.Count)
            {
                if(hand[i].Value == Values.Nine)
                {
                    foundFirstNine = true;
                }
                else
                {
                    i++;
                }
            }

            int doRandom = r.Next(100);

            if (foundFirstNine)
            {
                if (i > (hand.Count - 1) / 2)
                {
                    if (doRandom >= 15)
                    {
                        ps = playStyle.C;
                        realName = "Sarkhan Vol";
                    }
                    
                    if(doRandom < 15)
                    {
                        ps = playStyle.RL;
                        realName = "Sarkhan Le Fou";
                    }
                }

                if (i > (hand.Count - 1) / 2)
                {
                    if (doRandom >= 15)
                    {
                        ps = playStyle.D;
                        realName = "Sarkhan Inaltéré";
                    }

                    if (doRandom < 15)
                    {
                        ps = playStyle.RH;
                        realName = "Sarkhan Le Fou";
                    }
                }
            }
            else
            {
                if (doRandom < 15)
                {
                    ps = playStyle.RM;
                    realName = "Sarkhan Le Fou";
                }
            }
        }
    }
}
