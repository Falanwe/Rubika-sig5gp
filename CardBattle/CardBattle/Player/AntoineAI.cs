using CardBattle.Models;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using CardBattle.Player;

namespace CardBattle
{
    public class AntoineAI : IPlayer
    {
        private int _playerCount;
        private int _position;
        private List<Card> _cards;
        private Hashtable _winners;
        private int _points;
        private int _foldNotWon;
        private int _foldNotWonToPlayBetter;

        public string Name
        {
            get
            {
                return "Flantier";
            }
        }

        public string Author
        {
            get
            {
                return "Antoine";
            }
        }

        public void Initialize(int playerCount, int position)
        {
            _playerCount = playerCount;
            _position = position;
            _foldNotWon = 0;
            _foldNotWonToPlayBetter = 3;
        }

        public void Deal(IEnumerable<Card> cards)
        {
            _winners = new Hashtable();
            _cards = new List<Card>();
            _cards.AddRange(cards);
            _cards = Sort.QuickSort(_cards);
        }

        public Card PlayCard()
        {
            int indexToPlay = 0 ;

            if (_cards.Count <= 3)
            {
                indexToPlay = _cards.Count - 1;
            }
            else if (_foldNotWon >= _foldNotWonToPlayBetter)
            {
                indexToPlay = _cards.Count - 1;
                _foldNotWon = 0;
            }

            Card toReturn = _cards[indexToPlay];

            _cards.RemoveAt(indexToPlay);
            return toReturn;
        }

        public void ReceiveFoldResult(FoldResult result)
        {
            int winner = result.Winner;
            if (!_winners.ContainsKey(winner))
                _winners.Add(winner, 1);
            else
                _winners[winner] = (int)_winners[winner] + 1;
            if (winner == _position)
            {
                _points++;
                _foldNotWon = 0;
            }
            else
            {
                _foldNotWon++;
            }
        }
    }
}
