using System;
using System.Collections.Generic;
using System.Text;
using ConsoleNanoWallet.Rendering;

namespace ConsoleNanoWallet.Components
{
    public class StatusBarComponent : Component
    {
        public string Status { get; set; } = "";

        public int Padding { get; set; }
        private int a;

        public override void Render(StyledCharacter[] buffer, Style style)
        {
            // Find the bottom row character index
            a++;
            var bottomRowCharIndex = buffer.Length - Console.WindowWidth;

            // Set background to accent
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                if (i + bottomRowCharIndex < buffer.Length)
                {
                    buffer[i + bottomRowCharIndex] = new StyledCharacter(' ', new Style(style.AccentForeground, style.AccentBackground));
                }
            }

            var astring = a.ToString();
            // Write status
            var statusString = $"{Status} | {astring}";
            for (int i = 0; i < statusString.Length; i++)
            {
                if (i + bottomRowCharIndex < buffer.Length)
                {
                    buffer[i + bottomRowCharIndex].Character = statusString[i];
                }
            }
        }
    }
}
