using System;

namespace Wallet.Components
{
    public abstract class Component
    {
        public abstract void Render(char[] buffer, (ConsoleColor foreground, ConsoleColor background)[] colourBuffer, int screenWidth, Style style);
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Style? StyleOverride { get; set; }
    }
}