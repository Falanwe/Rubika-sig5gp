using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;
using CardBattle.Infrastructure;

namespace CardBattle.Player
{
    class BenoitV : IPlayer
    {
        private string _name = "Nephet";
        private string _author = "Benoit V";

        List<Card> _listOfCardInMyHand;
        Dictionary<int, List<Card>> _listOfCardPlayedByPlayer;

        private int nbOfWin = 0;
        private int nbOfTurn = 0;

        private int myPosition;
        private bool firstTier = false;

        public string Author
        {
            get
            {
                return _author;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public void Deal(IEnumerable<Card> cards)
        {
            _listOfCardInMyHand = new List<Card>();
            

            foreach (Card card in cards)
            {
                _listOfCardInMyHand.Add(card);
            }

            _listOfCardInMyHand = Sort.QuickSort(_listOfCardInMyHand);
        }

        public void Initialize(int playerCount, int position)
        {
            _listOfCardPlayedByPlayer = new Dictionary<int, List<Card>>();
            myPosition = position;

            List<Card> initList = new List<Card>();

            for(int i =0; i < playerCount; i++)
            {
                _listOfCardPlayedByPlayer.Add(i, initList);
            }


        }

        public Card PlayCard()
        {
            RandomProvider rnd = new RandomProvider() ;
            Card myCard;

            if(nbOfWin < _listOfCardInMyHand.Count/2 && nbOfTurn > ((int)Math.Round((double)(_listOfCardInMyHand.Count / 3))) && !firstTier)
            {
                firstTier = true;
                myCard = _listOfCardInMyHand.Last();
            }else
            {
                myCard = _listOfCardInMyHand.ElementAt(rnd.Random.Next((int) Math.Round((double)(_listOfCardInMyHand.Count/3))));
            }
            
            _listOfCardInMyHand.Remove(myCard);
            return myCard;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            nbOfTurn++;

            /*//all cards played by others players
            for (int i = 0; i < result.CardsPlayed.Count(); i++)
            {
                if(i != myPosition)
                {
                    List<Card> tempListResult = result.CardsPlayed.ToList();

                    List<Card> tempListPlayed = _listOfCardPlayedByPlayer[i];
                    tempListPlayed.Add(tempListResult.ElementAt(i));

                    _listOfCardPlayedByPlayer.Add(i, tempListPlayed);
                }
            }*/

            if(result.Winner == myPosition)
            {
                nbOfWin++;
            }
        }
    }
}
