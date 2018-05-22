using System;

namespace ConsoleNanoWallet
{
    public struct Style
    {
        public Style(ConsoleColor foreground, ConsoleColor background, ConsoleColor accentBackground, ConsoleColor accentForeground)
        {
            Foreground = foreground;
            Background = background;
            AccentBackground = accentBackground;
            AccentForeground = accentForeground;

        }

        public Style(ConsoleColor foreground, ConsoleColor background)
        {
            Foreground = foreground;
            Background = background;
            AccentBackground = ConsoleColor.White;
            AccentForeground = ConsoleColor.Black;
        }

        public ConsoleColor Foreground { get; set; }
        public ConsoleColor Background { get; set; }
        public ConsoleColor AccentBackground { get; set; }
        public ConsoleColor AccentForeground { get; set; }

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