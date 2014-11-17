using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    class Card
    {
        private enum CardRank
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }
        private enum CardSuit
        {
            Spade,Diamond,Heart,Club
        }
        public CardRank Rank { get; set; }
        public CardSuit Suit { get; set; }
        /// <summary>
        /// Creates a card with a certain Rank and Suit
        /// </summary>
        /// <param name="myRank">Rank based on enum value</param>
        /// <param name="mySuit">Suit based on enum value</param>
        public Card(int myRank, int mySuit)
        {
            this.Rank = (CardRank)myRank;
            this.Suit = (CardSuit)mySuit;
        }
        /// <summary>
        /// Override the ToString()
        /// </summary>
        /// <returns>Card rank of it's suit</returns>
        public string ToString()
        {
            return this.Rank +" of "+ this.Suit;
        }
    }
}
