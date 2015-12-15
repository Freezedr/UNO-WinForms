using System;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace UNO_WinForms // не забудьте поменять на свой namespace //
{

    public class MyHookClass : NativeWindow
    {

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);


        //public solver solv; // - пример включения класса "решателя"


        uint simpr;
        Form1 f;
        public Game g;
        public string currentSet;
        public bool fl;

        public MyHookClass(Form1 af)
        {
            simpr = RegisterWindowMessage("MyMessage");
            this.AssignHandle(af.Handle);
            g = new Game();
            fl = false;
            currentSet = g.s_cardstoplay;
        }

        protected override void WndProc(ref Message m) // в эту функцию приходят все сообщения от СИМПРА
        {
            int wparamhi, wparamlo, wparam;
            int lParam = Convert.ToInt32("" + m.LParam);

            if (m.Msg == simpr)
            {

                wparam = Convert.ToInt32("" + m.WParam);
                wparamhi = wparam / 65536;
                wparamlo = wparam - wparamhi * 65536;
                int index = g.number;

                #region conditions
                if (wparamhi == 0)//условия
                {
                    if (wparamlo == 2)// таблица 1 
                    {
                        g.print("Таблица 2, условия");
                        switch (lParam)
                        {
                            case 1: // У соперника Б одна карта?
                                index = g.dealer.pass_course(true, index);
                                m.Result = g.dealer.players[index].isOneCard();
                                //f.richtextBox1.Text += "СИМПР: У соперника Б одна карта?";
                                break;
                            case 2: // У соперника В одна карта?
                                index = g.dealer.pass_course(true, index);
                                index = g.dealer.pass_course(true, index);
                                m.Result = g.dealer.players[index].isOneCard();
                                //f.richtextBox1.Text += "СИМПР: У соперника В одна карта?";
                                break;
                            case 3: // У соперника Б меньше 4 карт?
                                index = g.dealer.pass_course(true, index);
                                m.Result = g.dealer.players[index].isLessFourCard();
                                //f.richtextBox1.Text += "СИМПР: У соперника Б меньше 4 карт?";
                                break;
                            case 4: // У соперника А меньше 4 карт?
                                index = g.dealer.pass_course(false, index);
                                m.Result = g.dealer.players[index].isLessFourCard();
                                //f.richtextBox1.Text += "СИМПР: У соперника А меньше 4 карт?";
                                break;
                            case 5: // Есть в руке "пропуск хода"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.Skip);
                                //f.richtextBox1.Text += "СИМПР: Есть в руке 'пропуск хода'?";
                                break;
                            case 6: // Есть в руке "разворот"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.Reverse);
                                //f.richtextBox1.Text += "СИМПР: Есть в руке 'разворот'?"; 
                                break;
                            case 7: // Есть в руке "+2"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.DrawTwo);
                                //f.richtextBox1.Text += "СИМПР: Есть в руке '+2'?"; 
                                break;
                            case 8: // Есть в руке "+4"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.WildFour);
                                //f.richtextBox1.Text += "СИМПР: Есть в руке '+4'?"; 
                                break;
                            default:
                                break;
                        }
                        fl = false;  // СИМПР не сделал ещё свой выбор
                    }
                    else if (wparamlo == 3)// таблица 2 
                    {
                        g.print("Таблица 3, условия");
                        switch (lParam)
                        {
                            case 1: // Есть в руке карта "пропуск хода"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.Skip);
                                break;
                            case 2: // Есть в руке карта "разворот"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.Reverse);
                                break;
                            case 3: // Есть в руке карта "+2"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.DrawTwo);
                                break;
                            case 4: // Есть в руке карта "+4"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.WildFour);
                                break;
                            case 5: // Есть в руке карта "заказать цвет"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.Wild);
                                break;
                            case 6: 
                                m.Result = g.dealer.players[g.number].hasCard(Values.Nine);
                                break;
                            case 7: 
                                m.Result = g.dealer.players[g.number].hasCard(Values.Eight);
                                break;
                            case 8: 
                                m.Result = g.dealer.players[g.number].hasCard(Values.Seven);
                                break;
                            case 9:
                                m.Result = g.dealer.players[g.number].hasCard(Values.Six);
                                break;
                            case 10: 
                                m.Result = g.dealer.players[g.number].hasCard(Values.Five);
                                break;
                            case 11: 
                                m.Result = g.dealer.players[g.number].hasCard(Values.Four);
                                break;
                            case 12: 
                                m.Result = g.dealer.players[g.number].hasCard(Values.Three);
                                break;
                            case 13: 
                                m.Result = g.dealer.players[g.number].hasCard(Values.Two); 
                                break;
                            case 14: 
                                m.Result = g.dealer.players[g.number].hasCard(Values.One);
                                break;
                            case 15: 
                                m.Result = g.dealer.players[g.number].hasCard(Values.Zero); 
                                break;
                            case 16: // Есть карты такого же цвета, как сейчас в игре?
                                Colours pileColor = g.dealer.pile.Peek().colour;
                                m.Result = g.dealer.players[g.number].isPileColour(pileColor);
                                break;
                            default: break;
                        }
                        fl = false;
                    }
                    else if (wparamlo == 1)
                    {
                        g.print("Таблица 1, условия");
                        switch (lParam)
                        {
                            case 1:
                                if (fl)
                                    m.Result = new IntPtr(1);    
                                m.Result = new IntPtr(0);
                                break;
                            default:
                                break;
                        } 
                    }
                    
                }
                #endregion

                #region actions
                else if (wparamhi == 1)//действия
                {
                    
                    if (wparamlo == 2)// таблица 1 
                    {
                       g.print("Таблица 2, действия");
                        switch (lParam)
                        {
                            case 1:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Skip, currentSet);
                                fl = true;
                                break;
                            case 2:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Reverse, currentSet);
                                fl = true;
                                break;
                            case 3:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.DrawTwo, currentSet);
                                fl = true;
                                break;
                            case 4:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.WildFour, currentSet);
                                fl = true;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (wparamlo == 3)// таблица 2 
                    {
                       g.print("Таблица 3, действия");
                        switch (lParam)
                        {
                            case 1: // Играть карту "разворот"
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Skip, currentSet);
                                fl = true;
                                break;
                            case 2: // Играть карту "разворот"
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Reverse, currentSet);
                                fl = true;
                                break;
                            case 3: // Играть карту "+2"
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.DrawTwo, currentSet);
                                fl = true;
                                break;
                            case 4: // Играть карту "+4"
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.WildFour, currentSet);
                                fl = true;
                                break;
                            case 5: // Играть карту "заказать цвет"
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Wild, currentSet);
                                fl = true;
                                break;
                            case 6:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Nine, currentSet);
                                fl = true;
                                break;
                            case 7:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Eight, currentSet);
                                fl = true;
                                break;
                            case 8:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Seven, currentSet);
                                fl = true;
                                break;
                            case 9:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Six, currentSet);
                                fl = true;
                                break;
                            case 10:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Five, currentSet);
                                fl = true;
                                break;
                            case 11:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Four, currentSet);
                                fl = true;
                                break;
                            case 12:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Three, currentSet);
                                fl = true;
                                break;
                            case 13:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Two, currentSet);
                                fl = true;
                                break;
                            case 14:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.One, currentSet);
                                fl = true;
                                break;
                            case 15:
                                Data.selectedCard = g.dealer.players[g.number].play_card(Values.Zero, currentSet);
                                fl = true;
                                break;
                            case 16: // Есть карты такого же цвета, как сейчас в игре?
                                currentSet = g.s_pilecolour;
                                break;
                            case 17:
                                Data.selectedCard = null;
                                fl = true;
                                break;
                            default: break;

                        }
                        
                    }
                    else if (wparamlo == 1)// таблица 3
                    {
                        g.print("Таблица 1, действия");
                        switch (lParam)
                        {
                            case 1:
                                fl = false;
                                break;
                            case 2:
                                fl = true;
                                break;
                            default:
                                break;
                        }
                    }

                    Application.DoEvents();
                    Thread.Sleep(300); // если у нас есть визуальное отображение, то задержку можно установить здесь                    
                    m.Result = new IntPtr(1); // ответом на запрос действия со стороны СИМПР должна быть 
                }
                #endregion
            }
            else
            {
                base.WndProc(ref m); // для всех действий не связанных с СИМПР возвращаем управление программе
            }



        }

    }
}
