using CardBattle.Models;
using CardBattle.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle.Infrastructure
{
    public class Game
    {
        private readonly List<IPlayer> _players;
        private readonly int _handSize;

        public int PlayersCount
        {
            get
            {
                return _players.Count;
            }
        }

        private readonly List<int> _scores;
        private readonly List<Card>[] _hands;

        private readonly CardDealer _dealer;
        private readonly ILogger _logger;

        public Game(CardDealer dealer, List<IPlayer> players, int handSize, ILogger logger)
        {
            _players = players;
            _handSize = handSize;
            _dealer = dealer;
            _logger = logger;

            _scores = new List<int>(players.Select(p => 0));
            _hands = new List<Card>[PlayersCount];
        }

        public int PlayGame()
        {
            _dealer.Shuffle();
            for (var i = 0; i < PlayersCount; i++)
            {
                _hands[i] = _dealer.Deal(_handSize);
                _players[i].Deal(_hands[i].AsReadOnly());
            }

            for (var i = 0; i < _handSize; i++)
            {
                var foldResult = PlayFold();

                _scores[foldResult.Winner]++;
            }

            return _scores.IndexOf(_scores.Max());
        }

        public FoldResult PlayFold()
        {
            List<Card> cardsPlayed = new List<Card>();
            for (var i = 0; i < PlayersCount; i++)
            {
                var card = _players[i].PlayCard();
                if (!_hands[i].Remove(card))
                {
                    throw new InvalidOperationException(_players[i].Name + "is a cheater!");
                }

                _logger.Log(LogLevel.Debug, card.ToString());
                cardsPlayed.Add(card);
            }

            var winnerIndex = cardsPlayed.IndexOf(cardsPlayed.Max());
            var result = new FoldResult(cardsPlayed.AsReadOnly(), winnerIndex, _players[winnerIndex].Name);
            foreach (var player in _players)
            {
                player.ReceiveFoldResult(result);
            }
            _logger.Log(LogLevel.Debug, "[" + string.Join(", ", result.CardsPlayed.Select(c => c.ToString()).ToArray()) + "] => " + result.Winner);
            return result;
        }
    }
}
