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
            Deck myDeck = new Deck();
            myDeck.Shuffle();
            Console.WriteLine(myDeck.DealACard().ToString());
            Console.WriteLine(myDeck.DealACard().ToString());
        }
    }
    /// <summary>
    /// A class that holds Cards and features a few helper methods
    /// </summary>
    class Deck
    {
        private List<Card> unusedCards = new List<Card>();
        private List<Card> dealtCards = new List<Card>();
        private Random rng = new Random();

        /// <summary>
        /// Constructor that initializes the deck of cards
        /// </summary>
        public Deck()
        {
            // Fill the unusedCards with a set of cards

            // Suits first
            for (int i = 0; i < 4; i++)
            {
                // Cards, which start with 2 and go till Ace (14)
                for (int j = 2; j < 15; j++)
                {
                    // Add this new card to the deck
                    unusedCards.Add(new Card(i, j));
                }
            }
        }
        /// <summary>
        /// Picks a random card then removes it from the unused list and adds it to the dealt list
        /// </summary>
        /// <returns>the random card to a player/hand</returns>
        public Card DealACard()
        {
            // Get a random card from the unused deck
            int randomIndex = rng.Next(GetNumOfCards());
            Card tempCard = unusedCards.ElementAt(randomIndex);
            //Put the card in the dealt deck
            dealtCards.Add(tempCard);
            // Remove card from the unused deck
            unusedCards.RemoveAt(randomIndex);

            // Return that card to the player/hand
            return tempCard;
        }

        /// <summary>
        /// Uses in list shuffling to reorder the existing cards
        /// </summary>
        public void Shuffle()
        {
            // When there is more then 1 card to shuffle
            if (unusedCards.Count > 1)
            {
                // Go through each index of the list
                for (int i = unusedCards.Count - 1; i >= 0; i--)
                {
                    // Pick a random card to swap with this index
                    Card tmp = unusedCards[i];
                    int randomIndex = rng.Next(i + 1);

                    //Swap elements
                    unusedCards[i] = unusedCards[randomIndex];
                    unusedCards[randomIndex] = tmp;
                }
            }
        }
        /// <summary>
        /// Returns the number of unused cards in the deck
        /// </summary>
        /// <returns>number of unused cards in the deck</returns>
        public int GetNumOfCards()
        {
            return unusedCards.Count();
        }
    }
    class Card
    {
        public enum CardRank
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
        public enum CardSuit
        {
            Spade,Diamond,Heart,Club
        }
        public CardRank Rank { get; set; }
        public CardSuit Suit { get; set; }
        /// <summary>
        /// Constructor creates a card with a certain Rank and Suit
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
        override public string ToString()
        {
            return this.Rank +" of "+ this.Suit;
        }
    }
}
