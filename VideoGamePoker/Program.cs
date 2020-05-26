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

            string welcome = "Welcome to Video Game Poker!\n";
            Console.SetCursorPosition((Console.WindowWidth - welcome.Length) / 2, Console.CursorTop);
            Console.WriteLine(welcome);
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

                var suits = new List<Card.Suit>();
                var values = new List<Card.FaceValue>();

                for (int i = 0; i < hand.Length; i++)
                {
                    suits.Add(hand[i].GetSuit());
                    values.Add(hand[i].GetFaceValue());
                }


                if (RoyalFlush(hand, values, suits))
                {
                    Console.WriteLine("Royal Flush! You win ${0}", royalFlush * amount);
                    bankRoll += royalFlush * amount;
                    Console.WriteLine("Bankroll: {0}\n", bankRoll);
                }
                else if (StraightFlush(hand, values, suits))
                {
                    Console.WriteLine("Straight Flush! You win ${0}", straightFlush * amount);
                    bankRoll += straightFlush * amount;
                    Console.WriteLine("Bankroll: {0}\n", bankRoll);
                }
                else if (FourOfAKind(hand, values, suits))
                {
                    Console.WriteLine("Four of a Kind! You win ${0}", fourOfAKind * amount);
                    bankRoll += fourOfAKind * amount;
                    Console.WriteLine("Bankroll: {0}\n", bankRoll);
                }
                else if (FullHouse(hand, values, suits))
                {
                    Console.WriteLine("Full House! You win ${0}", fullHouse * amount);
                    bankRoll += fullHouse * amount;
                    Console.WriteLine("Bankroll: {0}\n", bankRoll);
                }
                else if (Flush(hand, values, suits))
                {
                    Console.WriteLine("Flush! You win ${0}", flush * amount);
                    bankRoll += flush * amount;
                    Console.WriteLine("Bankroll: {0}\n", bankRoll);
                }
                else if (Straight(hand, values, suits))
                {
                    Console.WriteLine("Straight! You win ${0}", straight * amount);
                    bankRoll += straight * amount;
                    Console.WriteLine("Bankroll: {0}\n", bankRoll);
                }
                else if (ThreeOfAKind(hand, values, suits))
                {
                    Console.WriteLine("Three of a Kind! You win ${0}", threeOfAKind * amount);
                    bankRoll += threeOfAKind * amount;
                    Console.WriteLine("Bankroll: {0}\n", bankRoll);
                }
                else if (TwoPair(hand, values, suits))
                {
                    Console.WriteLine("Two Pair! You win ${0}", twoPair * amount);
                    bankRoll += twoPair * amount;
                    Console.WriteLine("Bankroll: {0}\n", twoPair);
                }
                else if (Pair(hand, values, suits))
                {
                    Console.WriteLine("Pair! You win ${0}", pair * amount);
                    bankRoll += pair * amount;
                    Console.WriteLine("Bankroll: {0}\n", bankRoll);
                }
                else
                {
                    Console.WriteLine("You Lose!");
                    Console.WriteLine("Bankroll: {0}\n", bankRoll);
                }

            }
        }

        // Gets the amount the player would like to bet
        static int GetAmount(int bankRoll)
        {
            int amount;
            Console.WriteLine("How much money would you like to bet?");
            Console.WriteLine("Current Bankroll: {0}\n", bankRoll);
            Console.Write("Amount: ");
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
            var seen = new List<int>();
            int count = 1;
            int index;

            while (true)
            {
                while (!int.TryParse(Console.ReadLine(), out index) || index > 5 || seen.Contains(index))
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

                seen.Add(index);
                count++;
            }


            return temp.ToArray();
        }

        // Player Hand Evaluation Methods

        static bool RoyalFlush(Card[] hand, List<Card.FaceValue> values, List<Card.Suit> suits)
        {
            if (values.Distinct().ToArray().Length == 1)
            {
                return true;
            }
            return false;
        }

        static bool StraightFlush(Card[] hand, List<Card.FaceValue> values, List<Card.Suit> suits)
        {
            return false;
        }

        static bool FourOfAKind(Card[] hand, List<Card.FaceValue> values, List<Card.Suit> suits)
        {
            if (values.Distinct().ToArray().Length == 2)
            {
                return true;
            }
            return false;
        }

        static bool FullHouse(Card[] hand, List<Card.FaceValue> values, List<Card.Suit> suits)
        {
            return false;
        }

        static bool Flush(Card[] hand, List<Card.FaceValue> values, List<Card.Suit> suits)
        {
            return false;
        }

        static bool Straight(Card[] hand, List<Card.FaceValue> values, List<Card.Suit> suits)
        {
            return false;
        }
        static bool ThreeOfAKind(Card[] hand, List<Card.FaceValue> values, List<Card.Suit> suits)
        {
            if (values.Distinct().ToArray().Length == 3)
            {
                return true;
            }
            return false;
        }
        static bool TwoPair(Card[] hand, List<Card.FaceValue> values, List<Card.Suit> suits)
        {
            return false;
        }
        static bool Pair(Card[] hand, List<Card.FaceValue> values, List<Card.Suit> suits)
        {
            if (values.Distinct().ToArray().Length == 4)
            {
                return true;
            }
            return false;
        }
    }
}
