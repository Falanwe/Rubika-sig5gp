using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;

namespace CardBattle.Player
{
	class WillPlayer : IPlayer
	{
		int myPosition = 0;
		int victoryCount = 0;
		int playerCount = 0;
		int turnNumber = 0;
		string name = "Will-IA";
		List<Card> localHandList;
		List<Card> outCard = new List<Card>();
		List<Card> allCard;
		List<Card> disponibleCard;
		static System.Random rnd = new Random();
		public string Author
		{
			get
			{
				return "William";
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public void Deal(IEnumerable<Card> cards)
		{
			if (localHandList == null)
				localHandList = new List<Card>(cards);
			else
				localHandList.AddRange(cards);
			localHandList = Sort.QuickSort<Card>(localHandList);
			outCard = new List<Card>();
			disponibleCard = new List<Card>(allCard);
			
		}

		public void Initialize(int pCount, int position)
		{
			myPosition = position;
			playerCount = pCount;
			allCard = new List<Card>();
			for (var i = 0; i < Enum.GetValues(typeof(Suit)).Length; i++)
			{
				for (var j = 0; j < Enum.GetValues(typeof(Values)).Length; j++)
				{
					allCard.Add(new Card((Values)j, (Suit)i));
				}
			}
		}

		public Card PlayCard()
		{
			Card card = chooseCard();
			localHandList.Remove(card);
			turnNumber++;
			return card;
		}

		public void ReceiveFoldResult(FoldResult result)
		{
			if(result.Winner == myPosition)
			{
				victoryCount++;
			}
			foreach (var item in result.CardsPlayed)
			{
				disponibleCard.Remove(item);
			}
		}
		
		Card chooseCard()
		{
			if (playerCount <= 2)
			{
				return localHandList[rnd.Next(localHandList.Count-1)];
			}
			else if(turnNumber <= 2)
			{
				return weakestCard();
			}
			else
			{
				if (probBestCard() < 1f )
					return weakestCard();
				else
					return strongestCard();
			}
		}

		Card strongestCard()
		{
			return localHandList[localHandList.Count-1];
		}

		Card weakestCard()
		{
			return localHandList[0];
		}

		/// <summary>
		/// return the probability that my best card is the best currently in game.
		/// </summary>
		/// <returns></returns>
		float probBestCard()
		{
			Card myBestCard = strongestCard();
			float numBest = 0;
			if(myBestCard!= null && disponibleCard.Count > 0)
			{
				foreach (Card item in disponibleCard)
				{
					if (myBestCard.CompareTo(item) >= 0) numBest++;
				}
			}
			numBest = numBest / disponibleCard.Count;
			return numBest;
		}
	}
}
