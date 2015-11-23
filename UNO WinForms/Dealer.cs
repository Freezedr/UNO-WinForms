﻿using System;
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
                    return true;
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
            if (forward)
                return number++ % 4;
            if (!forward && (number == 0) )
                return 3;

            return number-- % 4;  
        }

        public void pileToDeck()
        {
            while (pile.Count != 0)
            {
                Card item = pile.Pop();
                deck.Push(item);
            }
        }

        //public void printDeck()
        //{
        //    file.Write("Game deck: \n\n");
        //    for (int i = 0; i < deck.Count; i++)
        //    {
        //        Card curr = deck.ElementAt<Card>(i);
        //        file.Write(curr.ToString() + "\n");
        //    }
        //    file.Write("\n");
        //    //file.Close();
        //}

        public Stack<Card> deck; // колода
        public Stack<Card> pile; // сыгранные карты (бито)
        public List<Player> players;
        public System.IO.StreamWriter file;
    }
}
