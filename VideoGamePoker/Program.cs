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

            /*
             Game Loop

               Shuffle deck
               Get bet amount
               Reduce bankroll
               Deal cards
               Discard and deal new ones
               Evaluate hand
             */

            while (true)
            {

                // Shuffle Deck
                deck.Shuffle();

                // Get Player Bet Amount and Remove From Bankroll
                int amount = GetAmount(bankRoll);
                bankRoll -= amount;

                // Deal Cards
                Card[] hand = new Card[cardsInHand];
                Console.WriteLine("Here is your hand!");
                for (int i = 0; i < hand.Length; i++)
                {
                    hand[i] = deck.DealACard();
                }
                Console.WriteLine(" ");

                // Discard Cards
                hand = DiscardCards(hand);
                Console.WriteLine("Here is your hand after discard.");
                for (int i = 0; i < hand.Length; i++)
                {
                    Console.WriteLine(hand[i].ToString());
                }
                Console.WriteLine(" ");

                // Deal new Cards
                hand = DealNewCards(hand, deck);
                Console.WriteLine("Here is your new hand!");
                for (int i = 0; i < hand.Length; i++)
                {
                    Console.WriteLine(hand[i].ToString());
                }

                // Evaluate Hand / Add to Bankroll
                switch (EvaluateHand(hand))
                {
                    case 0:
                        Console.WriteLine("No Hand Detected");
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        Console.WriteLine("Four of a Kind!");
                        bankRoll += fourOfAKind;
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        Console.WriteLine("Three of a Kind!");
                        bankRoll += threeOfAKind;
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                }


            }
        }

        static int GetAmount(int bankRoll)
        {
            int amount;
            Console.WriteLine("How much money would you like to bet?");
            while (!int.TryParse(Console.ReadLine(), out amount) && amount < bankRoll)
            {
                Console.WriteLine("Invalid amount, please try again!");
            }
            return amount;
        }

        static Card[] DiscardCards(Card[] hand)
        {
            int limit = 4;
            int choice;

            Console.WriteLine("How many cards would you like to remove?");
            for (int i = 0; i < hand.Length; i++)
            {
                Console.WriteLine(hand[i].ToString());
            }

            while (!int.TryParse(Console.ReadLine(), out choice) && choice <= limit)
            {
                Console.WriteLine("Invalid Choice. Please Try Again.");
            }


            var temp = new List<Card>(hand);
            int index;

            for (int i = 0; i < choice; i++)
            {
                Console.WriteLine("Enter the index of the card you would like to remove.");
                while (!int.TryParse(Console.ReadLine(), out index) && index >= 5)
                {
                    Console.WriteLine("Invalid Index. Please Try Again!");
                }
                temp.RemoveAt(index);
            }
            return temp.ToArray();
        }

        static Card[] DealNewCards(Card[] hand, Deck deck)
        {
            int handLength = 5;

            var temp = new List<Card>(hand);
            if (hand.Length != handLength)
            {
                for (int i = 0; i < Math.Abs(hand.Length - handLength); i++)
                {
                    temp.Add(deck.DealACard());
                }
            }
            return temp.ToArray();
        }

        static int EvaluateHand(Card[] hand)
        {
            /*
             1 - Royal Flush
             2 - Straight Flush
             3 - Four of a Kind
             4 - Full House
             5 - Flush
             6 - Straight
             7 - Three of a Kind
             8 - Two Pair
             9 - Pair
             */

            // Royal Flush
            var royalFlush = new List<Card.Suit>();
            for (int i = 0; i < hand.Length; i++)
            {
                royalFlush.Add(hand[i].GetSuit());
            }


            // Straight Flush

            // Four of a Kind - Remove duplicates and check for Length
            var fourOfAKind = new List<Card.FaceValue>();
            for (int i = 0; i < hand.Length; i++)
            {
                fourOfAKind.Add(hand[i].GetFaceValue());
            }
            if (fourOfAKind.Distinct().ToArray().Length == 1)
            {
                return 3;
            }

            // Full House

            // Flush

            // Straight

            // Three of a Kind - Remove duplicates and check for Length
            var threeOfAKind = new List<Card.FaceValue>();
            for (int i = 0; i < hand.Length; i++)
            {
                threeOfAKind.Add(hand[i].GetFaceValue());
            }
            if (threeOfAKind.Distinct().ToArray().Length == 2)
            {
                return 7;
            }

            // Two Pair

            // Pair



            return 0;
        }
    }
}
