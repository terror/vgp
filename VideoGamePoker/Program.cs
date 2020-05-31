using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoGamePoker
{
    /// <summary>
    ///  Video Poker Game in C# Utilizing OOP, Functional Decomposition and Algorithms
    /// </summary>

    /*
     Created by Liam Scalzulli
     May 31, 2020

     Tester: Nicholas Nittolo
     */

    class Program
    {
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

        static void Main(string[] args)
        {
            const int cardsInHand = 5;
            int bankRoll = 1000;
            int amount;
            Deck deck = new Deck();
            int choice;



            string welcome = "Welcome to Video Game Poker!\n";
            Console.WriteLine(welcome);

            do
            {
                Console.WriteLine("1 - Play\n" +
                    "2 - Test\n" +
                    "3 - Quit\n");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        while (true)
                        {

                            // Shuffle Deck
                            deck.Shuffle();

                            // Get Player Bet Amount and Remove From Bankroll
                            amount = GetAmount(bankRoll);
                            int temp = bankRoll;


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

                            // Evaluate Hand / Add or Remove from Bankroll
                            bankRoll = DetectHand(hand, bankRoll, amount);

                            if (bankRoll == temp)
                            {
                                bankRoll -= amount;
                            }

                            if (bankRoll <= 0)
                            {
                                Console.WriteLine("You're all out of money! Thanks for playing!\n");
                                break;
                            }

                            Console.WriteLine("\nDo you wish to play again? N to quit, any other key to continue.");
                            if (Console.ReadLine() == "N")
                            {
                                break;
                            }

                        }
                        break;
                    case 2:
                        Console.WriteLine("\n1 - Test Royal Flush\n" +
                            "2 - Test Straight Flush\n" +
                            "3 - Test Four of a Kind\n" +
                            "4 - Test Full House\n" +
                            "5 - Test Flush\n" +
                            "6 - Test Straight\n" +
                            "7 - Test Three of a Kind\n" +
                            "8 - Test Two Pair\n" +
                            "9 - Test Pair\n");
                        int.TryParse(Console.ReadLine(), out int testChoice);
                        int testBankroll = 1000;
                        int testAmount = GetAmount(testBankroll);

                        switch (testChoice)
                        {
                            case 1:
                                // Test A, K, Q, J, T Same suit
                                Card[] testRoyalFlush = { new Card(Card.Suit.Spades, Card.FaceValue.Ace), new Card(Card.Suit.Spades, Card.FaceValue.King), new Card(Card.Suit.Spades, Card.FaceValue.Queen), new Card(Card.Suit.Spades, Card.FaceValue.Jack), new Card(Card.Suit.Spades, Card.FaceValue.Ten) };
                                DetectHand(testRoyalFlush, testBankroll, testAmount);
                                break;
                            case 2:
                                // Test 8, 7, 6, 5, 4
                                Card[] testStraightFlush = { new Card(Card.Suit.Diamonds, Card.FaceValue.Eight), new Card(Card.Suit.Diamonds, Card.FaceValue.Seven), new Card(Card.Suit.Diamonds, Card.FaceValue.Six), new Card(Card.Suit.Diamonds, Card.FaceValue.Five), new Card(Card.Suit.Diamonds, Card.FaceValue.Four) };
                                DetectHand(testStraightFlush, testBankroll, testAmount);
                                break;
                            case 3:
                                // Test Q, Q, Q, Q, 10
                                Card[] testFour = { new Card(Card.Suit.Spades, Card.FaceValue.Queen), new Card(Card.Suit.Hearts, Card.FaceValue.Queen), new Card(Card.Suit.Diamonds, Card.FaceValue.Queen), new Card(Card.Suit.Spades, Card.FaceValue.Queen), new Card(Card.Suit.Diamonds, Card.FaceValue.Ten) };
                                DetectHand(testFour, testBankroll, testAmount);
                                break;
                            case 4:
                                // Test 3, 3, 3, K, K
                                Card[] testFullHouse = { new Card(Card.Suit.Spades, Card.FaceValue.Three), new Card(Card.Suit.Hearts, Card.FaceValue.Three), new Card(Card.Suit.Diamonds, Card.FaceValue.Three), new Card(Card.Suit.Spades, Card.FaceValue.King), new Card(Card.Suit.Diamonds, Card.FaceValue.King) };
                                DetectHand(testFullHouse, testBankroll, testAmount);
                                break;
                            case 5:
                                // Test all cards of the same suit
                                Card[] testFlush = { new Card(Card.Suit.Spades, Card.FaceValue.Three), new Card(Card.Suit.Spades, Card.FaceValue.Two), new Card(Card.Suit.Spades, Card.FaceValue.Four), new Card(Card.Suit.Spades, Card.FaceValue.King), new Card(Card.Suit.Spades, Card.FaceValue.King) };
                                DetectHand(testFlush, testBankroll, testAmount);
                                break;
                            case 6:
                                // Test K, Q, J, 10, 9
                                Card[] testStraight = { new Card(Card.Suit.Spades, Card.FaceValue.King), new Card(Card.Suit.Hearts, Card.FaceValue.Queen), new Card(Card.Suit.Diamonds, Card.FaceValue.Jack), new Card(Card.Suit.Spades, Card.FaceValue.Ten), new Card(Card.Suit.Diamonds, Card.FaceValue.Nine) };
                                DetectHand(testStraight, testBankroll, testAmount);
                                break;
                            case 7:
                                // Test 8, 8, 8, J, 5
                                Card[] testThree = { new Card(Card.Suit.Spades, Card.FaceValue.Eight), new Card(Card.Suit.Hearts, Card.FaceValue.Eight), new Card(Card.Suit.Diamonds, Card.FaceValue.Eight), new Card(Card.Suit.Spades, Card.FaceValue.Jack), new Card(Card.Suit.Diamonds, Card.FaceValue.Five) };
                                DetectHand(testThree, testBankroll, testAmount);
                                break;
                            case 8:
                                // Test Q, Q, 7, 7, A
                                Card[] testTwoPair = { new Card(Card.Suit.Spades, Card.FaceValue.Queen), new Card(Card.Suit.Hearts, Card.FaceValue.Queen), new Card(Card.Suit.Diamonds, Card.FaceValue.Seven), new Card(Card.Suit.Spades, Card.FaceValue.Seven), new Card(Card.Suit.Diamonds, Card.FaceValue.Ace) };
                                DetectHand(testTwoPair, testBankroll, testAmount);
                                break;
                            case 9:
                                // Test J, J, 10, 6, 2
                                Card[] testPair = { new Card(Card.Suit.Spades, Card.FaceValue.Jack), new Card(Card.Suit.Hearts, Card.FaceValue.Jack), new Card(Card.Suit.Diamonds, Card.FaceValue.Seven), new Card(Card.Suit.Spades, Card.FaceValue.Three), new Card(Card.Suit.Diamonds, Card.FaceValue.Ace) };
                                DetectHand(testPair, testBankroll, testAmount);
                                break;
                        }

                        break;
                    case 3:
                        break;
                }
            } while (choice <= 2 && choice > 0);
            // Game Loop

        }

        // Gets the amount the player would like to bet
        static int GetAmount(int bankRoll)
        {
            int amount;
            Console.WriteLine("\nHow much money would you like to bet?");
            Console.WriteLine("Current Bankroll: {0}\n", bankRoll);
            Console.Write("Amount: ");

            while (!int.TryParse(Console.ReadLine(), out amount) || amount > bankRoll)
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

        static bool RoyalFlush(Card[] hand)
        {
            SortValues(hand);
            // Highest straight flush
            if (Straight(hand) && Flush(hand) && hand[4].GetFaceValue() == Card.FaceValue.Ace)
            {
                return true;
            }
            return false;

        }

        static bool StraightFlush(Card[] hand)
        {
            if (Straight(hand) && Flush(hand))
            {
                return true;
            }
            return false;
        }

        static bool FourOfAKind(Card[] hand)
        {
            // 4 cards == in value, sort and check
            SortValues(hand);
            if (hand[0].GetFaceValue() == hand[1].GetFaceValue() && hand[1].GetFaceValue() == hand[2].GetFaceValue() && hand[2].GetFaceValue() == hand[3].GetFaceValue())
            {
                return true;
            }
            if (hand[1].GetFaceValue() == hand[2].GetFaceValue() && hand[2].GetFaceValue() == hand[3].GetFaceValue() && hand[3].GetFaceValue() == hand[4].GetFaceValue())
            {
                return true;
            }
            return false;

        }

        static bool FullHouse(Card[] hand)

        {
            SortValues(hand);
            if (hand[0].GetFaceValue() == hand[1].GetFaceValue() && hand[1].GetFaceValue() == hand[2].GetFaceValue() && hand[3].GetFaceValue() == hand[4].GetFaceValue())
            {
                return true;
            }
            if (hand[0].GetFaceValue() == hand[1].GetFaceValue() && hand[2].GetFaceValue() == hand[3].GetFaceValue() && hand[3].GetFaceValue() == hand[4].GetFaceValue())
            {
                return true;
            }
            return false;

        }

        static bool Flush(Card[] hand)
        {
            SortSuits(hand);
            // Check for same suit cards 0 -> 4 (All 5)
            if (hand[0].GetSuit() == hand[4].GetSuit())
            {
                return true;
            }
            return false;
        }

        static bool Straight(Card[] hand)
        {
            SortValues(hand);
            var temp = hand[0].GetFaceValue() + 1;

            // Check for sequence
            for (int i = 1; i < hand.Length; i++)
            {
                if (hand[i].GetFaceValue() != temp)
                {
                    return false;
                }
                temp += 1;
            }

            return true;
        }
        static bool ThreeOfAKind(Card[] hand)
        {
            // Possible cases: cccab, acccb, abccc

            SortValues(hand);
            if (hand[0].GetFaceValue() == hand[1].GetFaceValue() && hand[1].GetFaceValue() == hand[2].GetFaceValue())
            {
                return true;
            }
            if (hand[1].GetFaceValue() == hand[2].GetFaceValue() && hand[2].GetFaceValue() == hand[3].GetFaceValue())
            {
                return true;
            }
            if (hand[2].GetFaceValue() == hand[3].GetFaceValue() && hand[3].GetFaceValue() == hand[4].GetFaceValue())
            {
                return true;
            }
            return false;
        }
        static bool TwoPair(Card[] hand)
        {
            // Possible cases: aabbc, aacbb, caabb

            if (hand[0].GetFaceValue() == hand[1].GetFaceValue() && hand[2].GetFaceValue() == hand[3].GetFaceValue())
            {
                return true;
            }

            if (hand[0].GetFaceValue() == hand[1].GetFaceValue() && hand[3].GetFaceValue() == hand[4].GetFaceValue())
            {
                return true;
            }

            if (hand[1].GetFaceValue() == hand[2].GetFaceValue() && hand[3].GetFaceValue() == hand[4].GetFaceValue())
            {
                return true;
            }

            return false;
        }
        static bool Pair(List<Card.FaceValue> values)
        {
            // If only one duplicate in values, a pair exists
            if (values.Distinct().ToArray().Length == 4)
            {
                return true;
            }
            return false;
        }

        // Selection sort hand by suit
        public static void SortSuits(Card[] hand)
        {
            for (int i = 0; i < hand.Length; i++)
            {
                int m = i;
                for (int j = i + 1; j < hand.Length; j++)
                {
                    if (hand[j].GetSuit() < hand[m].GetSuit())
                    {
                        m = j;
                    }
                }
                // Swap cards
                Card temp = hand[i];
                hand[i] = hand[m];
                hand[m] = temp;
            }


        }

        // Selection sort hand by values
        public static void SortValues(Card[] hand)
        {
            for (int i = 0; i < hand.Length; i++)
            {
                int m = i;
                for (int j = i + 1; j < hand.Length; j++)
                {
                    if (hand[j].GetFaceValue() < hand[m].GetFaceValue())
                    {
                        m = j;
                    }
                }
                // Swap cards
                Card temp = hand[i];
                hand[i] = hand[m];
                hand[m] = temp;
            }
        }

        // Detect hand method for playing and testing
        static int DetectHand(Card[] hand, int bankRoll, int amount)
        {
            var values = new List<Card.FaceValue>();

            for (int i = 0; i < hand.Length; i++)
            {
                values.Add(hand[i].GetFaceValue());

            }

            if (RoyalFlush(hand))
            {
                Console.WriteLine("Royal Flush! You win ${0}", royalFlush * amount);
                bankRoll += royalFlush * amount;
                Console.WriteLine("Bankroll: {0}\n", bankRoll);
            }
            else if (StraightFlush(hand))
            {
                Console.WriteLine("Straight Flush! You win ${0}", straightFlush * amount);
                bankRoll += straightFlush * amount;
                Console.WriteLine("Bankroll: {0}\n", bankRoll);
            }
            else if (FourOfAKind(hand))
            {
                Console.WriteLine("Four of a Kind! You win ${0}", fourOfAKind * amount);
                bankRoll += fourOfAKind * amount;
                Console.WriteLine("Bankroll: {0}\n", bankRoll);
            }
            else if (FullHouse(hand))
            {
                Console.WriteLine("Full House! You win ${0}", fullHouse * amount);
                bankRoll += fullHouse * amount;
                Console.WriteLine("Bankroll: {0}\n", bankRoll);
            }
            else if (Flush(hand))
            {
                Console.WriteLine("Flush! You win ${0}", flush * amount);
                bankRoll += flush * amount;
                Console.WriteLine("Bankroll: {0}\n", bankRoll);
            }
            else if (Straight(hand))
            {
                Console.WriteLine("Straight! You win ${0}", straight * amount);
                bankRoll += straight * amount;
                Console.WriteLine("Bankroll: {0}\n", bankRoll);
            }
            else if (ThreeOfAKind(hand))
            {
                Console.WriteLine("Three of a Kind! You win ${0}", threeOfAKind * amount);
                bankRoll += threeOfAKind * amount;
                Console.WriteLine("Bankroll: {0}\n", bankRoll);
            }
            else if (TwoPair(hand))
            {
                Console.WriteLine("Two Pair! You win ${0}", twoPair * amount);
                bankRoll += twoPair * amount;
                Console.WriteLine("Bankroll: {0}\n", bankRoll);
            }
            else if (Pair(values))
            {
                Console.WriteLine("Pair! You win ${0}", pair * amount);
                bankRoll += pair * amount;
                Console.WriteLine("Bankroll: {0}\n", bankRoll);
            }
            else
            {
                Console.WriteLine("You Lose!");
                Console.WriteLine("Bankroll: {0}\n", bankRoll - amount);
            }
            return bankRoll;
        }
    }
}
