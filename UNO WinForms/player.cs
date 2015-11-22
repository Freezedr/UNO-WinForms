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
        }

        public void simpr_talking()
        {
            
        }

        public Card play_card(Card pile)
        {
            foreach (Card card in hand)
            {
                if ( (card.colour == pile.colour) ||
                     (card.colour == Colours.Wild) ||
                     (card.value == pile.value)  )
                    cardsToPlay.Add(card);
            }



            return null;
        }

        public List<Card> hand;
        public List<Card> cardsToPlay;
    }
}
