using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNO_WinForms
{
    public class Game
    {
        public Game()
        {
            file = new System.IO.StreamWriter("log.txt");
            dealer = new Dealer();
            s_pilecolour = "pileColourCards";
            s_cardstoplay = "cardstoplay";
            printDeck();
            printHands();
        }

        public void play(Form1 f)
        {
            number = 0;  // порядковый номер игрока
            bool forward = true; // направление игры
            // отрисовка игровой ситуации
            f.paint_position();
            MessageBox.Show("Включите СИМПР");
            // очистка игрового поля для новых положений
            f.clear_position();
            f.richTextBox1.Text += "Pile card: " + dealer.pile.Peek().ToString() + '\n';
            // играем
            while (!dealer.gameFinished())
            {
                f.paint_position();
                
                // если закончилась игровая колода - переворачиваем бито
                if (dealer.deck.Count == 0)
                    dealer.pileToDeck();
                if ((dealer.deck.Count == 0) || (dealer.pile.Count == 0))
                    return;
                
                // составление множества карт, из которых можно сыграть 
                dealer.players[number].cardsToPlay_init(dealer.pile.Peek());
                // игрок советуется с СИМПР
                while (!f.mhk.fl)
                {
                    Application.DoEvents();
                }
                f.richTextBox1.Text += Data.str_buf;
                Data.str_buf = "";
                Card selectedCard = Data.selectedCard;
                // игрок не сыграл карту -> берёт карту из колоды
                if (selectedCard == null)
                {
                    dealer.players[number].hand.Add(dealer.deck.Peek());
                    f.richTextBox1.Text += "player " + number.ToString() + " takes: " + dealer.deck.Peek().ToString() + '\n';
                    dealer.deck.Pop();
                }
                // игрок сыграл карту
                Random RND = new Random();
                if (selectedCard != null)
                {
                    f.richTextBox1.Text += "player " + number.ToString() + " card: " + selectedCard.ToString() + '\n';
                    switch (selectedCard.value)
                    {
                        case Values.Skip:
                            number = dealer.pass_course(forward, number);
                            break;
                        case Values.Reverse:
                            // играем в другую сторону
                            forward = false;
                            break;
                        case Values.DrawTwo:
                            // следующий игрок пропускает ход
                            number = dealer.pass_course(forward, number);
                            // и берёт две карты
                            dealer.players[number].hand.Add(dealer.deck.Peek());
                            dealer.deck.Pop();
                            dealer.players[number].hand.Add(dealer.deck.Peek());
                            dealer.deck.Pop();
                            break;
                        case Values.Wild:
                            // случайным образом задаётся цвет
                            selectedCard.colour = (Colours)RND.Next(4);
                            f.richTextBox1.Text += "New colour: " + selectedCard.colour + '\n';
                            break;
                        case Values.WildFour:
                            // случайным образом задаётся цвет
                            selectedCard.colour = (Colours)RND.Next(4);
                            f.richTextBox1.Text += "New colour: " + selectedCard.colour + '\n';
                            // + штраф следующему игроку в 4 карты
                            for (int i = 0; i < 3; i++)
                            {
                                dealer.players[number].hand.Add(dealer.deck.Peek());
                                dealer.deck.Pop();
                            }
                            break;
                        default:
                            break;
                    }
                    // игрок кладёт свою карту в стопку 
                    dealer.pile.Push(selectedCard);
                    dealer.players[number].hand.Remove(selectedCard);
                }
                // и передаёт ход следующему
                f.mhk.fl = false;
                number = dealer.pass_course(forward, number);
                f.clear_position();
                //f.paint_position();
            }
            f.paint_position();
        }

        public void printDeck()
        {
            file.Write("Game deck: \n\n");
            for (int i = 0; i < dealer.deck.Count; i++)
            {
                Card curr = dealer.deck.ElementAt<Card>(i);
                file.Write(curr.ToString() + "\n");
            }
            file.Write("\n");
            //file.Close();
        }

        public void printHands()
        {
            for (int i = 0; i < dealer.players.Count; i++)
            {
                file.Write("\nPlayer " + i + " hand: \n\n");
                file.Write(dealer.players[i].hand.Count.ToString() + "\n");
                for (int j = 0; j < dealer.players[i].hand.Count; j++)
                {
                    Card curr = dealer.players[i].hand.ElementAt<Card>(j);
                    file.Write(curr.ToString() + "\n");
                }
            }
            file.Write("\n");
            file.Close();
        }

        public void print(string str)
        {
            Data.str_buf += ""; // str + '\n';
        }

        public int number;
        public Dealer dealer;
        public string s_pilecolour;
        public string s_cardstoplay;
        public System.IO.StreamWriter file;
    }
}
