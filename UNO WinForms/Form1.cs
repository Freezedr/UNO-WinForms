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
            InitializeComponent();
            dealer = new Dealer();
            play();
        }

        public void play()
        {
            int number = 0;  // порядковый номер игрока
            bool forward = false; // направление игры
            while (!dealer.gameFinished())
            {
                //MyHookClass mhk = new MyHookClass(this);
                Card selectedCard = dealer.players[number].play_card(dealer.pile.Peek());
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

        public Dealer dealer;
    }
}
