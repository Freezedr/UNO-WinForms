using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UNO_WinForms
{
    public class Dealer
    {
        public Dealer()
        {
            players = new List<Player>();
            pile = new Stack<Card>();

            // составляем колоду
            List<Card> items = new List<Card>();

            // добавляем в колоду дублирующиеся карты
            for (Colours i = Colours.Red; 
                 i < Colours.Wild; 
                 i++)
            {
                for (Values j = Values.One; 
                    j < Values.Wild; 
                    j++)
                {
                    Card card = new Card();
                    card.colour = (Colours)i;
                    card.value = j;
                    items.Add(card);
                    items.Add(card);
                }
            }
            // добавляем в колоду остальные карты
            for (int i = 0; i < 4; i++)
            {
                // добавляем нули
                Card card = new Card();
                card.colour = (Colours)i;
                card.value = Values.Zero;
                items.Add(card); 
                // дикие карты "заказать цвет"
                card.colour = Colours.Wild;
                card.value = Values.Wild;
                items.Add(card);
                // дикие карты "+4"
                card.value = Values.WildFour;
                items.Add(card);
            }

            // перемешиваем колоду
            Random RND = new Random();
            for (int i = 0; i < items.Count; i++)
            {
                Card tmp = items[0];
                items.RemoveAt(0);
                items.Insert(RND.Next(items.Count), tmp);
            }
            deck = new Stack<Card>(items);
           
            // раздаём карты игрокам
            init_hands();
            
        }

        // определяет конец игры
        public bool gameFinished()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].hand.Count == 0)
                    return true;
            }
            return false;
        }

        public void init_hands()
        {
            Player player = new Player();

            for (int i = 0; i < 4; i++)
            {
                players.Add(player);    
            }
                        
            // каждому игроку даём по семь карт
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < players.Count; j++)
                {
                    players[j].hand.Add(deck.Peek());
                    deck.Pop();
                }
            }

            // кладём верхнюю карту колоды в бито
            Card first_card = deck.Peek();
            // но она не должна быть "дикой"
            if (first_card.colour == Colours.Wild)
            {
                bool legalStart = false;
                while (!legalStart)
                {
                    Random RND = new Random();
                    int rand = RND.Next(deck.Count);
                    deck.Pop();
                    Stack<Card> tmpList = new Stack<Card>();
                    for (int i = 0; i < rand; i++)
                    {
                        Card tempCard = deck.Peek();
                        deck.Pop();
                        tmpList.Push(tempCard);
                    }
                    first_card = deck.Peek();
                    if (first_card.colour != Colours.Wild)
                        legalStart = true;
                    else
                        tmpList.Clear();
                }
            }

            pile.Push(first_card);
        }
        
        public int pass_course(bool forward, int number)
        {
            if (forward)
                return number++ % 4;
            if (!forward && (number == 0) )
                return 3;

            return number-- % 4;  
        }

        public Stack<Card> deck; // колода
        public Stack<Card> pile; // сыгранные карты (бито)
        public List<Player> players;
    }
}
