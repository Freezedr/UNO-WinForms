using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UNO_WinForms
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Data.selectedCard = new Card();
            Data.selectedCard.colour = Colours.Blue;
            Data.selectedCard.value = Values.One;
            Application.Run(new Form1());
        }
        
    }

    static class Data
    {
        public static Card selectedCard;
        public static string str_buf;
    }

    
}
