using System;

namespace ConsoleNanoWallet
{
    public struct Style
    {
        public Style(ConsoleColor foreground, ConsoleColor background, ConsoleColor accent)
        {
            Foreground = foreground;
            Background = background;
            Accent = accent;
        }

        public Style(ConsoleColor foreground, ConsoleColor background)
        {
            Foreground = foreground;
            Background = background;
            Accent = ConsoleColor.White;
        }

        public ConsoleColor Foreground { get; set; }
        public ConsoleColor Background { get; set; }
        public ConsoleColor Accent { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Style s && s.Background == this.Background && s.Foreground == this.Foreground;
        }

        public static bool operator ==(Style a, Style b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Style a, Style b)
        {
            return !(a == b);
        }
    }
}