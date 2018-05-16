using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleNanoWallet.Rendering
{
    public struct StyledCharacter
    {
        public StyledCharacter(char character, Style style)
        {
            Character = character;
            Style = style;
        }

        public StyledCharacter(char character, ConsoleColor foreground, ConsoleColor background) 
        {
            Character = character;
            Style = new Style(foreground, background);
        }

        public char Character { get; set; }
        public Style Style { get; set; }
    }
}
