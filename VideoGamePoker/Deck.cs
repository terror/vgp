using System;
using System.Text;

namespace VideoGamePoker
{
    public class Deck
    {
        private const int TOTAL_CARDS = 52;

        private Card[] deck = new Card[TOTAL_CARDS];

        private int currentCard = 0;

        private Random random;

        public Deck()
        {
            random = new Random();      //generate a list of random numbers to be used in the shuffle

            int num = 0;
            foreach (Card.Suit aSuitValue in Enum.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.FaceValue aFaceValue in Enum.GetValues(typeof(Card.FaceValue)))
                {
                    deck[num++] = new Card(aSuitValue, aFaceValue);
                }
            }
        }

        public void Shuffle()
        {
            currentCard = 0;            //reset the currentCard index for each round. 
            int randomPosition;

            for (int i = 0; i < deck.Length; i++)
            {
                randomPosition = random.Next(deck.Length);
                Swap(deck, i, randomPosition);
            }
        }

        //Swap method
        //Input: Card array, int cardPosition, int cardPositionTwo
        //Output: none
        //Cards are objects and the array of cards holds the address (references) of those Card objects.
        //Swapping method changes the addresses of those cards in the array. 
        private void Swap(Card[] deck, int cardPositionOne, int cardPositionTwo)
        {
            Card temp = deck[cardPositionOne];
            deck[cardPositionOne] = deck[cardPositionTwo];
            deck[cardPositionTwo] = temp;
        }

        //DealACard method
        //Pre-Condtion - the deck cannot be empty.
        //IsEmpty is verified in the application before calling DealACard()
        //Input: none
        //Outputs: none
        //Returns the next card in the deck. 
        public Card DealACard()
        {
            return deck[currentCard++];
        }

        public bool IsEmpty()
        {
            return currentCard > TOTAL_CARDS - 1;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (Card c in deck)
            {
                builder.AppendLine(c.ToString());
            }

            return builder.ToString();
        }
    }
}
