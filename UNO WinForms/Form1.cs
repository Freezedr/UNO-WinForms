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
            mhk = new MyHookClass(this);
            mhk.fl = false;
        }

        public MyHookClass mhk;

        private void button1_Click(object sender, EventArgs e)
        {
            mhk.g.play(this);
        }

        PictureBox[][] hands;
        PictureBox pile;
        PictureBox arrow;

        public void paint_position()
        {
            hands = new PictureBox[4][];
            // рисуем карты игроков
            for (int i = 0; i < mhk.g.dealer.players.Count; i++)
            {
                int handSize = mhk.g.dealer.players[i].hand.Count;
                PictureBox[] hand = new PictureBox[handSize];
                for (int j = 0; j < handSize; j++)
                {
                    hand[j] = new PictureBox();
                    hand[j].Location = getCardPoint(i, j);
                    hand[j].Size = new Size(77, 120);
                    hand[j].Image = getImage(mhk.g.dealer.players[i].hand[j]);
                    hand[j].Visible = true;
                    this.Controls.Add(hand[j]);
                }
                hands[i] = hand;
            }
            // карту в стопке
            pile = new PictureBox();
            pile.Location = new Point(425, 200);
            pile.Size = new Size(77, 120);
            pile.Image = getImage(mhk.g.dealer.pile.Peek());
            pile.Visible = true;
            this.Controls.Add(pile);
            // и стрелку
            arrow = new PictureBox();
            arrow.Location = getArrowPoint(mhk.g.number);
            arrow.Size = new Size(50, 50);
            arrow.Image = UNO_WinForms.Properties.Resources.arrow;
            arrow.Visible = true;
            this.Controls.Add(arrow);
        }

        public void clear_position()
        {
            foreach (var hand in hands)
            {
                foreach (var card in hand)
                {
                    card.Dispose();
                }
            }
            pile.Dispose();
            arrow.Dispose();
        }

        private Point getArrowPoint(int number)
        {
            return new Point(360, 50 + number * 120);
        }

        private Point getCardPoint(int playerIndex, int cardIndex)
        {
            return new Point(12 + cardIndex * 28, 12 + playerIndex * 120);
        }

        private Image getImage(Card card)
            {
                switch (card.value)
                {
                    case Values.Zero:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_0;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_0;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_0;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_0;
                            default:
                                break;
                        }
                        break;
                    case Values.One:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_1;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_1;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_1;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_1;
                            default:
                                break;
                        }
                        break;
                    case Values.Two:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_2;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_2;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_2;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_2;
                            default:
                                break;
                        }
                        break;
                    case Values.Three:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_3;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_3;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_3;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_3;
                            default:
                                break;
                        }
                        break;
                    case Values.Four:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_4;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_4;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_4;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_4;
                            default:
                                break;
                        }
                        break;
                    case Values.Five:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_5;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_5;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_5;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_5;
                            default:
                                break;
                        }
                        break;
                    case Values.Six:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_6;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_6;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_6;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_6;
                            default:
                                break;
                        }
                        break;
                    case Values.Seven:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_7;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_7;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_7;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_7;
                            default:
                                break;
                        }
                        break;
                    case Values.Eight:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_8;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_8;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_8;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_8;
                            default:
                                break;
                        }
                        break;
                    case Values.Nine:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_9;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_9;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_9;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_9;
                            default:
                                break;
                        }
                        break;
                    case Values.Skip:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_stop;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_stop;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_stop;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_stop;
                            default:
                                break;
                        }
                        break;
                    case Values.Reverse:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_reverse;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_reverse;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_reverse;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_reverse;
                            default:
                                break;
                        }
                        break;
                    case Values.DrawTwo:
                        switch (card.colour)
                        {
                            case Colours.Red:
                                return UNO_WinForms.Properties.Resources.red_draw_2;
                            case Colours.Yellow:
                                return UNO_WinForms.Properties.Resources.yellow_draw_2;
                            case Colours.Blue:
                                return UNO_WinForms.Properties.Resources.blue_draw_2;
                            case Colours.Green:
                                return UNO_WinForms.Properties.Resources.green_draw_2;
                            default:
                                break;
                        }
                        break;
                    case Values.Wild:
                        return UNO_WinForms.Properties.Resources.wild;
                    case Values.WildFour:
                        return UNO_WinForms.Properties.Resources.wild_four;
                    default:
                        break;
                }
                // не должны попадать сюда
                return null;
            }
        
    }
}
