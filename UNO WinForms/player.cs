using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UNO_WinForms
{    
    public class Player
    {
        public Player()
        {
            hand = new List<Card>();
            cardsToPlay = new List<Card>();
            pileColourCards = new List<Card>();
        }

        public void simpr_talking()
        {
            
        }

        public Card play_card(Values value, string cardSet)
        {
            if (cardSet == "cardstoplay")
            {
                foreach (Card item in cardsToPlay)
                {
                    if (item.value == value)
                    {
                        hand.Remove(item);
                        return item;
                    }
                }    
            }
            if (cardSet == "pileColourCards")
            {
                foreach (Card item in pileColourCards)
                {
                    if (item.value == value)
                    {
                        hand.Remove(item);
                        return item;
                    }
                }    
            }
            
            return null;
        }

        public void pileColourCards_init(Values value, Colours colour)
        {
            pileColourCards.Clear();
            foreach (Card item in cardsToPlay)
            {
                if (item.colour == colour)
                    pileColourCards.Add(item);
            }
        }

        public void cardsToPlay_init(Card pile)
        {
            cardsToPlay.Clear();
            foreach (Card card in hand)
            {
                if ((card.colour == pile.colour) ||
                     (card.colour == Colours.Wild) ||
                     (card.value == pile.value))
                    cardsToPlay.Add(card);
            }
        }

        public IntPtr hasCard (Values value)
        {
            foreach (Card item in cardsToPlay)
            {
                if (item.value == value)
                    return new IntPtr(1);
            }
            return new IntPtr(0);
        }

        public IntPtr isOneCard()
        {
            if (hand.Count == 1)
                return new IntPtr(1);
            return new IntPtr(0);
        }

        public IntPtr isLessFourCard()
        {
            if (hand.Count < 4)
                return new IntPtr(1);
            return new IntPtr(0);
        }

        public IntPtr isPileColour(Colours colour)
        {
            foreach (Card item in cardsToPlay)
            {
                if (item.colour == colour)
                    return new IntPtr(1);
            }
            return new IntPtr(0);
        }

        public List<Card> hand;
        public List<Card> cardsToPlay;
        public List<Card> pileColourCards;
    }
}
