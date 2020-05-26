using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoGamePoker
{
    class Program
    {
        static void Main(string[] args)
        {
            const int cardsInHand = 5;
            int bankRoll = 1000;
            Deck deck = new Deck();

            // Payouts
            const int royalFlush = 250;
            const int straightFlush = 50;
            const int fourOfAKind = 25;
            const int fullHouse = 9;
            const int flush = 6;
            const int straight = 4;
            const int threeOfAKind = 3;
            const int twoPair = 2;
            const int pair = 1;
            //

            Console.WriteLine("Welcome to Video Game Poker!\n");
            while (true)
            {

                // Shuffle Deck
                deck.Shuffle();

                // Get Player Bet Amount and Remove From Bankroll
                int amount = GetAmount(bankRoll);
                bankRoll -= amount;

                // Deal Cards
                Card[] hand = new Card[cardsInHand];
                Console.WriteLine("Here is your hand!\n");
                for (int i = 0; i < hand.Length; i++)
                {
                    hand[i] = deck.DealACard();
                    Console.WriteLine(hand[i].ToString());
                }
                Console.WriteLine(" ");

                // Replace Cards
                hand = ReplaceCards(hand, deck);
                Console.WriteLine("Here is your hand after replace.\n");
                for (int i = 0; i < hand.Length; i++)
                {
                    Console.WriteLine(hand[i].ToString());
                }
                Console.WriteLine(" ");

                // Evaluate Hand / Add to Bankroll
                switch (EvaluateHand(hand))
                {
                    case 0:
                        Console.WriteLine("You Lose.\n");
                        break;
                    case 1:
                        Console.WriteLine("Royal Flush! You win ${0}\n", royalFlush * amount);
                        bankRoll += royalFlush * amount;
                        Console.WriteLine("Bankroll: {0}", bankRoll);
                        break;
                    case 2:
                        Console.WriteLine("Straight Flush! You win ${0}\n", straightFlush * amount);
                        bankRoll += straightFlush * amount;
                        Console.WriteLine("Bankroll: {0}", bankRoll);
                        break;
                    case 3:
                        Console.WriteLine("Four of a Kind! You win ${0}\n", fourOfAKind * amount);
                        bankRoll += fourOfAKind * amount;
                        Console.WriteLine("Bankroll: {0}", bankRoll);
                        break;
                    case 4:
                        Console.WriteLine("Full House! You win ${0}\n", fullHouse * amount);
                        bankRoll += fullHouse * amount;
                        Console.WriteLine("Bankroll: {0}", bankRoll);
                        break;
                    case 5:
                        Console.WriteLine("Flush! You win ${0}\n", flush * amount);
                        bankRoll += flush * amount;
                        Console.WriteLine("Bankroll: {0}", bankRoll);
                        break;
                    case 6:
                        Console.WriteLine("Straight! You win ${0}\n", straight * amount);
                        bankRoll += straight * amount;
                        Console.WriteLine("Bankroll: {0}", bankRoll);
                        break;
                    case 7:
                        Console.WriteLine("Three of a Kind! You win ${0}\n", threeOfAKind * amount);
                        bankRoll += threeOfAKind * amount;
                        Console.WriteLine("Bankroll: {0}", bankRoll);
                        break;
                    case 8:
                        Console.WriteLine("Two Pair! You win ${0}\n", twoPair * amount);
                        bankRoll += royalFlush * amount;
                        Console.WriteLine("Bankroll: {0}", twoPair);
                        break;
                    case 9:
                        Console.WriteLine("Pair! You win ${0}\n", pair * amount);
                        bankRoll += pair * amount;
                        Console.WriteLine("Bankroll: {0}", bankRoll);
                        break;
                }


            }
        }

        // Gets amount player would like to bet
        static int GetAmount(int bankRoll)
        {
            int amount;
            Console.WriteLine("How much money would you like to bet?");
            Console.WriteLine("Current Bankroll: {0}", bankRoll);
            while (!int.TryParse(Console.ReadLine(), out amount) && amount < bankRoll)
            {
                Console.WriteLine("Invalid amount, please try again!");
            }
            return amount;
        }

        // Allows player to replace up to 4 cards in hand with new cards from deck
        static Card[] ReplaceCards(Card[] hand, Deck deck)
        {

            Console.WriteLine("Choose which cards to replace, Press 0 to Exit");
            for (int i = 0; i < hand.Length; i++)
            {
                Console.WriteLine("[{0}] {1}", i + 1, hand[i].ToString());
            }


            var temp = new List<Card>(hand);
            int count = 0;
            int index;

            while (true)
            {
                while (!int.TryParse(Console.ReadLine(), out index) && index >= 5)
                {
                    Console.WriteLine("Invalid Index. Please Try Again!");
                }

                if (index == 0 || count == 4)
                {
                    break;
                }
                temp.RemoveAt(index - 1);
                temp.Insert(index - 1, deck.DealACard());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Card Successfully Replaced...");
                Console.ResetColor();
                count++;
            }


            return temp.ToArray();
        }

        // Evaluates player hand and detects for a winning hand
        static int EvaluateHand(Card[] hand)
        {

            // Suits and Values for Evaluation
            var suits = new List<Card.Suit>();
            var values = new List<Card.FaceValue>();

            for (int i = 0; i < hand.Length; i++)
            {
                suits.Add(hand[i].GetSuit());
                values.Add(hand[i].GetFaceValue());
            }

            // Royal Flush

            // Straight Flush

            // Four of a Kind - Remove duplicates and check for Length

            if (values.Distinct().ToArray().Length == 2)
            {
                return 3;
            }

            // Full House

            // Flush

            // Straight

            // Three of a Kind - Remove duplicates and check for Length

            if (values.Distinct().ToArray().Length == 3)
            {
                return 7;
            }

            // Two Pair

            // Pair
            if (values.Distinct().ToArray().Length == 4)
            {
                return 9;
            }

            return 0;
        }
    }
}
