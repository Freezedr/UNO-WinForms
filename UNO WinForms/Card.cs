using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UNO_WinForms
{
    public enum Colours { Red, Yellow, Blue, Green, Wild }
    public enum Values
    {
        Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine,
        Skip, Reverse, DrawTwo, Wild, WildFour
    }

    public class Card
    {
        public Card()
        {

        }

        public Colours colour;
        public Values value;
    }
}
