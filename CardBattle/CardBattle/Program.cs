using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardBattle.Models;
using System.IO;
using Autofac;
using CardBattle.Infrastructure;
using CardBattle.Player;
using CardBattle.Infrastructure;

namespace CardBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            MaintTournament();
            //MainCard();
            //MainFibo();
        }

        private static void MaintTournament()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CardDealer>();
            builder.RegisterType<RandomProvider>().SingleInstance();
            builder.RegisterInstance(new ConsoleLogger { MinLevel = LogLevel.None }).As<ILogger>().SingleInstance();
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(t => t.IsAssignableTo<IPlayer>()).As<IPlayer>();
            builder.RegisterType<TournamentOrganiser>();

            var container = builder.Build();

            //var dealer = container.Resolve<CardDealer>();

            var orga = container.Resolve<TournamentOrganiser>();
                //new TournamentOrganiser(
                //new IPlayer[]
                //{ new MaxValuePlayer(), new MaxValuePlayer() },
                //dealer
                //);

            orga.PlayTournament();

            Console.ReadLine();
        }

        private static void MainFibo()
        {
            var i = 0;
            Console.WriteLine("Fibo:");
            foreach (var f in Fibo.FirstFibos(90))
            {
                Console.WriteLine(f);
            }

            Console.WriteLine("10° multiple de 5 de fibo: " + Fibo.NthMultipleFibo(10, 5));

            Console.WriteLine("les diviseurs premiers de 23 sont: " + string.Join(", ", Fibo.Divisors(23).Select(d => d.ToString()).ToArray()));

            Console.WriteLine("les diviseurs premiers des 15 premiers nombres de fibonnaccis multiples de 3 sont: " +
                string.Join(", ", Fibo.DivisorsFromFibo(15, 3).Select(d => d.ToString()).ToArray())
                );

            Console.ReadLine();
        }

        public static void MainCard()
        {

            Func<Values, Card> heartsFactory = (v => new Card(v, Suit.Hearts));

            var nineOfHearts = heartsFactory(Values.Nine);

            var spadesAce = new Card(Values.Ace, Suit.Spades);

            Console.WriteLine("I created a card: " + spadesAce);

            var dealer = new CardDealer(new RandomProvider());
            for (var i = 0; i < 100; i++)
            {
                var deck = new List<Card>();
                for (var j = 0; j < 10; j++)
                {
                    dealer.Shuffle();

                    deck.AddRange(dealer.Deal(52));
                }

                deck.Sort();

                //Console.WriteLine("My hand contains " + string.Join(", ", hand.Select(c => c.ToString()).ToArray()));
            }

            Console.WriteLine("It took me " + ComparisonMetrics.Instance.ComparisonCount + " comparisons to sort 100 decks with .Net's built-in sort.");

            ComparisonMetrics.Instance.Reset();
            for (var i = 0; i < 100; i++)
            {
                var deck = new List<Card>();
                for (var j = 0; j < 10; j++)
                {
                    dealer.Shuffle();

                    deck.AddRange(dealer.Deal(52));
                }

                Sort.Insertion(deck);

                //Console.WriteLine("My hand contains " + string.Join(", ", hand.Select(c => c.ToString()).ToArray()));

            }
            Console.WriteLine("It took me " + ComparisonMetrics.Instance.ComparisonCount + " comparisons to sort 100 decks with the insertion sort.");

            ComparisonMetrics.Instance.Reset();
            for (var i = 0; i < 100; i++)
            {
                var deck = new List<Card>();
                for (var j = 0; j < 10; j++)
                {
                    dealer.Shuffle();

                    deck.AddRange(dealer.Deal(52));
                }
                Sort.Bubble(deck);
                //Console.WriteLine("My hand contains " + string.Join(", ", hand.Select(c => c.ToString()).ToArray()));

            }
            Console.WriteLine("It took me " + ComparisonMetrics.Instance.ComparisonCount + " comparisons to sort 100 decks with the bubble sort.");

            ComparisonMetrics.Instance.Reset();
            for (var i = 0; i < 100; i++)
            {
                var deck = new List<Card>();
                for (var j = 0; j < 10; j++)
                {
                    dealer.Shuffle();

                    deck.AddRange(dealer.Deal(52));
                }
                Sort.Shake(deck);
                //Console.WriteLine("My hand contains " + string.Join(", ", hand.Select(c => c.ToString()).ToArray()));

            }
            Console.WriteLine("It took me " + ComparisonMetrics.Instance.ComparisonCount + " comparisons to sort 100 decks with the shake sort.");

            ComparisonMetrics.Instance.Reset();
            for (var i = 0; i < 100; i++)
            {
                var deck = new List<Card>();
                for (var j = 0; j < 10; j++)
                {
                    dealer.Shuffle();

                    deck.AddRange(dealer.Deal(52));
                }
                deck = Sort.MergeSort(deck);
                //Console.WriteLine("My hand contains " + string.Join(", ", hand.Select(c => c.ToString()).ToArray()));

            }
            Console.WriteLine("It took me " + ComparisonMetrics.Instance.ComparisonCount + " comparisons to sort 100 decks with the merge sort.");


            ComparisonMetrics.Instance.Reset();
            for (var i = 0; i < 100; i++)
            {
                var deck = new List<Card>();
                for (var j = 0; j < 10; j++)
                {
                    dealer.Shuffle();

                    deck.AddRange(dealer.Deal(52));
                }
                deck = Sort.QuickSort(deck);
                //Console.WriteLine("My hand contains " + string.Join(", ", hand.Select(c => c.ToString()).ToArray()));

            }
            Console.WriteLine("It took me " + ComparisonMetrics.Instance.ComparisonCount + " comparisons to sort 100 decks with the quick sort.");

            ComparisonMetrics.Instance.Reset();
            for (var i = 0; i < 100; i++)
            {
                var deck = new List<Card>();
                for (var j = 0; j < 10; j++)
                {
                    dealer.Shuffle();

                    deck.AddRange(dealer.Deal(52));
                }
                deck = deck.OrderBy(c => c).ToList();
                //Console.WriteLine("My hand contains " + string.Join(", ", hand.Select(c => c.ToString()).ToArray()));

            }
            Console.WriteLine("It took me " + ComparisonMetrics.Instance.ComparisonCount + " comparisons to sort 100 decks with the Linq sort.");


            Console.ReadLine();

        }

    }
}
