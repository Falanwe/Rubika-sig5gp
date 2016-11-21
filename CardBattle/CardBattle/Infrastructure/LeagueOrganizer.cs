using CardBattle.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Infrastructure
{
    public class LeagueOrganizer
    {
        private MatchDescriptionList _config = new MatchDescriptionList
        {
            { 2, 5 },
            { 2, 15 },
            { 2, 25 },
            { 3, 5 },
            { 3, 10 },
            { 4, 5 },
            { 4,10 },
        };
        private readonly CardDealer _dealer;
        private readonly ILogger _logger;
        private readonly IEnumerable<IPlayer> _players;
        private readonly Dictionary<IPlayer, int> _scores = new Dictionary<IPlayer, int>();

        public LeagueOrganizer(IEnumerable<IPlayer> players, CardDealer dealer, ILogger logger)
        {
            _dealer = dealer;
            _logger = logger;
            _players = players;
        }

        public void RunLeague()
        {
            foreach(var description in _config)
            {
                RunTournament(description);
            }


            foreach (var player in _players.OrderByDescending(p => _scores[p]))
            {
                _logger.Log(LogLevel.Warning, "player " + player.Name + " from " + player.Author + ": " + _scores[player] + " games won");
            }
        }

        public void RunTournament(MatchDescription description)
        {
            var combinations = _players.AllTuples(description.PlayerCount);

            foreach (var combination in combinations)
            {
                var localPlayers = combination.ToList();
                var orga = new TournamentOrganiser(localPlayers, _dealer, _logger);

                orga.PlayTournament();

                int i = 0;
                foreach (var s in orga.Scores)
                {
                    int _;
                    if (!_scores.TryGetValue(localPlayers[i], out _))
                    {
                        _scores[localPlayers[i]] = 0;
                    }
                    _scores[localPlayers[i]] += s;

                    i++;
                }
            }

        }
    }
}
