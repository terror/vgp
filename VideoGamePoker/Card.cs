using System;
namespace VideoGamePoker
{
    public class Card
    {

        public enum FaceValue { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
        // Note: Ace is both the lowest and highest card
        public enum Suit { Hearts, Diamonds, Clubs, Spades };

        private FaceValue _faceValue;
        private Suit _suit;

        public Card()
        {
            _suit = Suit.Spades;
            _faceValue = FaceValue.Ace;
        }

        public Card(Suit theSuit_, FaceValue theFaceValue_)
        {
            _suit = theSuit_;
            _faceValue = theFaceValue_;
        }

        public override string ToString()
        {
            return _faceValue + " of " + _suit;
        }

        public Suit GetSuit()
        {
            return _suit;
        }

        public FaceValue GetFaceValue()
        {
            return _faceValue;
        }
    }
}
