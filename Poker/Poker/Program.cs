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
            Deck currentDeck = new Deck();
            currentDeck.Shuffle();
            PokerPlayer player1 = new PokerPlayer();
            //player1.DrawHand(currentDeck.Deal5Cards());
            List<Card> testHand = new List<Card>();
            testHand.Add(new Card((int)Card.CardRank.Ten, (int)Card.CardSuit.Club));
            testHand.Add(new Card((int)Card.CardRank.Jack, (int)Card.CardSuit.Club));
            testHand.Add(new Card((int)Card.CardRank.Queen, (int)Card.CardSuit.Club));
            testHand.Add(new Card((int)Card.CardRank.King, (int)Card.CardSuit.Club));
            testHand.Add(new Card((int)Card.CardRank.Ace, (int)Card.CardSuit.Club));
            player1.DrawHand(testHand);
            player1.ShowHand();

            Console.ReadKey();
        }
    }
    class PokerPlayer
    {
        private List<Card> Hand { get; set; }
        public enum HandType
        {
            HighCard,OnePair,TwoPair,ThreeOfAKind,Straight,Flush,FullHouse,FourOfAKind,StraightFlush,RoyalFlush
        }
        public HandType HandRank
        {
            get
            {
                if (HasRoyalFlush()) { return HandType.RoyalFlush;}
                if (HasStraightFlush()) { return HandType.StraightFlush; }
                if (HasFourOfAKind()) { return HandType.FourOfAKind; }
                if (HasFullHouse()) { return HandType.FullHouse; }
                if (HasFlush()) { return HandType.Flush; }
                if (HasStraight()) { return HandType.Straight; }
                if (HasThreeOfAKind()) { return HandType.ThreeOfAKind; }
                if (HasTwoPair()) { return HandType.TwoPair; }
                if (HasPair()) { return HandType.OnePair; }
                return HandType.HighCard;
            }
        }
        
        public PokerPlayer() { }
        public void DrawHand(List<Card> cards)
        {
            this.Hand = cards;
        }
        public void ShowHand()
        {
            switch (this.HandRank)
            {
                case HandType.HighCard: Console.WriteLine("You have a highcard");
                    break;
                case HandType.OnePair: Console.WriteLine("You have one pair");
                    break;
                case HandType.TwoPair: Console.WriteLine("You have two pair");
                    break;
                case HandType.ThreeOfAKind: Console.WriteLine("You have three of a kind");
                    break;
                case HandType.Straight: Console.WriteLine("You have a straight");
                    break;
                case HandType.Flush: Console.WriteLine("You have a Flush");
                    break;
                case HandType.FullHouse: Console.WriteLine("You have a Full House");
                    break;
                case HandType.FourOfAKind: Console.WriteLine("You have Four of a Kind");
                    break;
                case HandType.StraightFlush: Console.WriteLine("You have a straight flush");
                    break;
                case HandType.RoyalFlush: Console.WriteLine("You have a Royal Flush");
                    break;
            }
            // Print out each card in a hand
            foreach (Card aCard in Hand)
            {
                Console.WriteLine(string.Join(" ", aCard.ToString()));
            }
        }
        /// <summary>
        /// A function to look for a certain number of cards with the same rank
        /// </summary>
        /// <param name="numberOfCards">certain number of cards needed exactly</param>
        /// <returns>true if the exact number is found</returns>
        private bool HasCertainNumOfCards(int numberOfCards)
        {
            return this.Hand.GroupBy(x => x.Rank).Where(grouping => grouping.Count() == 2).Count() == numberOfCards;
        }
        public bool HasPair()
        {
            // Only one pair exactly
            return HasCertainNumOfCards(1);
        }
        public bool HasTwoPair()
        {
            return HasCertainNumOfCards(2);
        }
        public bool HasThreeOfAKind()
        {
            return this.Hand.GroupBy(x => x.Rank).Where(grouping => grouping.Count() == 3).Any();
        }
        public bool HasStraight()
        {
            // Make sure all of the cards are distinct
            if(!HasPair()){
                // Ace is used as the low card (has Ace and 2), check for straight
                if(this.Hand.OrderBy(x=>x.Rank).Last().Rank == Card.CardRank.Ace) {
                    List<Card> tempHand = Hand.OrderBy(x=>x.Rank).ToList();
                    // Remove the Ace
                    tempHand.RemoveAt(tempHand.Count - 1);
                    // Then check for the straight with 2,3,4,5
                    return (tempHand.OrderBy(x => x.Rank).Last().Rank - tempHand.OrderBy(x => x.Rank).First().Rank)==3;                    
                } else {
                    // Check for normal straight
                    return (this.Hand.OrderBy(x => x.Rank).Last().Rank - this.Hand.OrderBy(x => x.Rank).First().Rank) == 4;
                }
            } // If there are any duplicate cards, then it can't be a straight
            else { return false; }
        }
        public bool HasFlush()
        {
            return this.Hand.GroupBy(x=>x.Suit).Count() == 1;
        }

        public bool HasFullHouse()
        {
            return HasThreeOfAKind() && HasPair();
        }
        public bool HasFourOfAKind()
        {
            return HasCertainNumOfCards(4);
        }
        public bool HasStraightFlush()
        {
            return HasStraight() && HasFlush();
        }
        public bool HasRoyalFlush()
        {
            // Check that there is a Ten and an Ace
            if ((this.Hand.Where(x => x.Rank == Card.CardRank.Ten).Count() == 1) && (this.Hand.Where(x => x.Rank == Card.CardRank.Ace).Count() == 1))
            {
                return HasStraightFlush();
            } // if those cards aren't in the hand it can't be a royal flush
            else { return false; }
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
                    unusedCards.Add(new Card(j, i));
                }
            }
        }
        /// <summary>
        /// Returns a list of 5 cards to make up a hand for the PokerPlayer
        /// </summary>
        /// <returns>list of 5 cards</returns>
        public List<Card> Deal5Cards()
        {
            // Return 5 cards to the PokerPlayer
            List<Card> tempHand = new List<Card>();
            for (int i = 0; i < 5; i++)
			{
                tempHand.Add(this.DealACard());			 
			}
            return tempHand;
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
            return this.Rank +" of "+ this.Suit.ToString();
        }
    }
}
