using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNO_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            file = new System.IO.StreamWriter("log.txt");
            mhk = new MyHookClass(this);
            InitializeComponent();
        }

        public void play()
        {
            number = 0;  // порядковый номер игрока
            bool forward = false; // направление игры
            while (!dealer.gameFinished())
            {
                if (dealer.deck.Count == 0)
                    dealer.pileToDeck();
                dealer.players[number].cardsToPlay_init(dealer.pile.Peek());
                mhk = new MyHookClass(this);
                Card selectedCard = mhk.selectedCard;
                //richTextBox1.Text += "player " + number.ToString() + " card: " + selectedCard.ToString();
                // игрок не сыграл карту -> берёт карту из колоды
                if (selectedCard == null)
                {
                    dealer.players[number].hand.Add(dealer.deck.Peek());
                    dealer.deck.Pop();
                }
                // игрок сыграл карту
                if (selectedCard != null)
                {
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
                            break;
                        case Values.WildFour:
                            break;
                        default:
                            break;
                    }
                    dealer.pile.Push(selectedCard);
                    dealer.players[number].hand.Remove(selectedCard);
                }
                number = dealer.pass_course(forward, number);
            }
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

        public int number;
        public Dealer dealer;
        public MyHookClass mhk;
        public string s_pilecolour;
        public string s_cardstoplay;
        public System.IO.StreamWriter file;

        private void button1_Click(object sender, EventArgs e)
        {
            dealer = new Dealer();
            s_pilecolour = "pileColourCards";
            s_cardstoplay = "cardstoplay";
            printDeck();
            printHands();
            play();
        } 
    }
}
