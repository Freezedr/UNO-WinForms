using System;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace UNO_WinForms // не забудьте поменять на свой namespace //
{

    public class MyHookClass : NativeWindow
    {
        //
        //   Класс работы с сообщениями.
        //   Автор: Кузнецов Андрей (А-16-05)
        //
        //   Примечание: для выполнение действий в этом классе рекомендуется создать 
        //   элемент класса, который используется для работы основной программы 
        //   например: public solver solv; // в объявлениях
        //   тогда в WndProc доступ к функциям будет как solv.function(...)
        //   а доступ из основной программы(с формы) к значеним и функциям решателя
        //   будет осуществлён так:
        //   MyHookClass hook; // в пространстве имён формы
        //   hook= new MyHookClass(); // в инициализаторе формы
        //   hook.solv.function(...); // доступ к значениям и функциям (например из таймера, для обновления графики)
        //   
        //  

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);


        //public solver solv; // - пример включения класса "решателя"


        uint simpr;
        Form1 f;
        Game g;
        public Card selectedCard;
        public string currentSet;

        public MyHookClass(Form1 af)
        {
            simpr = RegisterWindowMessage("MyMessage");
            this.AssignHandle(af.Handle);
            f = af;
            g = new Game(f);
            currentSet = g.s_cardstoplay;
            g.play();
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
                    if (wparamlo == 1)// таблица 1 
                    {
                        switch (lParam)
                        {
                            case 1: // У соперника Б одна карта?
                                index = g.dealer.pass_course(true, index);
                                m.Result = g.dealer.players[index].isOneCard(); 
                                break;
                            case 2: // У соперника В одна карта?
                                index = g.dealer.pass_course(true, index);
                                index = g.dealer.pass_course(true, index);
                                m.Result = g.dealer.players[index].isOneCard(); 
                                break;
                            case 3: // У соперника Б меньше 4 карт?
                                index = g.dealer.pass_course(true, index);
                                m.Result = g.dealer.players[index].isLessFourCard();
                                break;
                            case 4: // У соперника А меньше 4 карт?
                                index = g.dealer.pass_course(false, index);
                                m.Result = g.dealer.players[index].isLessFourCard();
                                break;
                            case 5: // Есть в руке "пропуск хода"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.Skip); 
                                break;
                            case 6: // Есть в руке "разворот"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.Reverse);
                                break;
                            case 7: // Есть в руке "+2"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.DrawTwo);
                                break;
                            case 8: // Есть в руке "+4"?
                                m.Result = g.dealer.players[g.number].hasCard(Values.WildFour);
                                break;
                            default:
                                break;
                        }
                        
                        //if (lParam == 1)// таблица 1 условие 1
                        //{
                        //    m.Result = new IntPtr(1); // вернуть условие 1 = TRUE
                        //}
                        //else if (lParam == 2)// таблица 1 условие 2
                        //{
                        //    m.Result = new IntPtr(0); // вернуть условие 2 = FALSE
                        //}
                    }
                    else if (wparamlo == 2)// таблица 2 
                    {
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
                    }
                }
                #endregion

                #region actions
                else if (wparamhi == 1)//действия
                {
                    
                    if (wparamlo == 1)// таблица 1 
                    {
                        selectedCard = new Card();
                        switch (lParam)
                        {
                            case 1:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Skip, currentSet);
                                break;
                            case 2:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Reverse, currentSet);
                                break;
                            case 3:
                                selectedCard = g.dealer.players[g.number].play_card(Values.DrawTwo, currentSet);
                                break;
                            case 4:
                                selectedCard = g.dealer.players[g.number].play_card(Values.WildFour, currentSet);
                                break;
                            default:
                                break;
                        }
                        Thread.Sleep(3000);
                    }
                    else if (wparamlo == 2)// таблица 2 
                    {
                        switch (lParam)
                        {
                            case 1: // Играть карту "разворот"
                                selectedCard = g.dealer.players[g.number].play_card(Values.Skip, currentSet);
                                break;
                            case 2: // Играть карту "разворот"
                                selectedCard = g.dealer.players[g.number].play_card(Values.Reverse, currentSet);
                                break;
                            case 3: // Играть карту "+2"
                                selectedCard = g.dealer.players[g.number].play_card(Values.DrawTwo, currentSet);
                                break;
                            case 4: // Играть карту "+4"
                                selectedCard = g.dealer.players[g.number].play_card(Values.WildFour, currentSet);
                                break;
                            case 5: // Играть карту "заказать цвет"
                                selectedCard = g.dealer.players[g.number].play_card(Values.Wild, currentSet);
                                break;
                            case 6:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Nine, currentSet);
                                break;
                            case 7:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Eight, currentSet);
                                break;
                            case 8:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Seven, currentSet);
                                break;
                            case 9:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Six, currentSet);
                                break;
                            case 10:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Five, currentSet);
                                break;
                            case 11:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Four, currentSet);
                                break;
                            case 12:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Three, currentSet);
                                break;
                            case 13:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Two, currentSet);
                                break;
                            case 14:
                                selectedCard = g.dealer.players[g.number].play_card(Values.One, currentSet);
                                break;
                            case 15:
                                selectedCard = g.dealer.players[g.number].play_card(Values.Zero, currentSet);
                                break;
                            case 16: // Есть карты такого же цвета, как сейчас в игре?
                                currentSet = g.s_pilecolour;
                                break;
                            case 17:
                                break;
                            default: break;

                        }
                    }

                    Application.DoEvents();
                    Thread.Sleep(3000); // если у нас есть визуальное отображение, то задержку можно установить здесь                    
                    m.Result = new IntPtr(1); // ответом на запрос действия со стороны СИМПР должна быть единица
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
