using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                    Card card1= new Card();
                    card1.colour = (Colours)i;
                    card1.value = j;
                    items.Add(card1);
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
                Card wild1 = new Card();
                wild1.colour = Colours.Wild;
                wild1.value = Values.Wild;
                items.Add(wild1);
                // дикие карты "+4"
                Card wild2 = new Card();
                wild2.value = Values.WildFour;
                wild2.colour = Colours.Wild;
                items.Add(wild2);
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

            //printDeck();

            // раздаём карты игрокам
            init_hands();
            
        }

        // определяет конец игры
        public bool gameFinished()
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].hand.Count == 0)
                {
                    MessageBox.Show("GAME!!!");
                    return true;
                }
                    
            }
            return false;
        }

        public void init_hands()
        {
            Player player = new Player();
            players.Add(player);
            Player player1 = new Player();
            players.Add(player1);
            Player player2 = new Player();
            players.Add(player2);
            Player player3 = new Player();
            players.Add(player3);
            
                        
            // каждому игроку даём по семь карт
            for (int i = 0; i < 7; i++)
            {
                    Card card = deck.Pop();
                    players[0].hand.Add(card);                    
            }

            for (int i = 0; i < 7; i++)
            {
                Card card = deck.Pop();
                players[1].hand.Add(card);
            }

            for (int i = 0; i < 7; i++)
            {
                Card card = deck.Pop();
                players[2].hand.Add(card);
            }
            
            for (int i = 0; i < 7; i++)
            {
                Card card = deck.Pop();
                players[3].hand.Add(card);
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
            if (forward && number < 3)
            {
                number++;
                return number;
            }
            if (forward && number == 3)
            {
                number = 0;
                return number;
            }
            if (!forward && number > 0)
            {
                number--;
                return number;
            }
            if (!forward && number == 0)
            {
                number = 3;
                return number;
            }
            return number;

        }

        public void pileToDeck()
        {
            while (pile.Count != 0)
            {
                Card item = pile.Pop();
                deck.Push(item);
            }
        }

        public Stack<Card> deck; // колода
        public Stack<Card> pile; // сыгранные карты (бито)
        public List<Player> players;
        public System.IO.StreamWriter file;
    }
}
