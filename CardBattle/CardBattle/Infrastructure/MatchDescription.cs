using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Infrastructure
{
    public struct MatchDescription
    {
        public int PlayerCount { get; private set; }
        public int HandSize { get; set; }

        public MatchDescription(int playerCount, int handsize)
        {
            PlayerCount = playerCount;
            HandSize = handsize;
        }
    }

    public class MatchDescriptionList : List<MatchDescription>
    {
        public void Add(int playerCount, int handsize)
        {
            Add(new MatchDescription(playerCount, handsize));
        }
    }
}
