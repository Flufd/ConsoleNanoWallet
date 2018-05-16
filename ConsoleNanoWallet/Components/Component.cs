using ConsoleNanoWallet.Rendering;
using System;

namespace ConsoleNanoWallet.Components
{
    public abstract class Component
    {
        /// <summary>
        /// Render the component to the screen buffer, writes the components characters to the buffer
        /// </summary>
        /// <param name="buffer">The buffer to write to</param>
        /// <param name="style">The style to write with</param>
        public abstract void Render(StyledCharacter[] buffer, Style style);

        /// <summary>
        /// Screen position of the component on X axis, (0 is left hand side)
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        /// Screen position of the component on Y axis, (0 is top)
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        /// The width of the component
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the component
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// An override style to use, if not provided it will use the passed style when rendering
        /// </summary>
        public Style? StyleOverride { get; set; }

        /// <summary>
        /// Render a character in local coordinates
        /// </summary>
        /// <param name="buffer">The buffer to render to</param>
        /// <param name="colourBuffer">The colour buffer to render to</param>
        /// <param name="character">The character to render</param>
        /// <param name="foreground">The foreground to draw with</param>
        /// <param name="background">The background to draw with</param>
        /// <param name="positionX">Local position of the character on X axis</param>
        /// <param name="positionY">Local position of the character Y axis</param>
        protected void RenderLocalCharacter(StyledCharacter[] buffer, StyledCharacter character, int positionX, int positionY)
        {
            // Offset by component position
            var screenPositionX = positionX + PositionX;
            var screenPositionY = positionY + PositionY;

            // Try to draw the component
            var bufferPosition = GetBufferPosition(screenPositionX, screenPositionY);

            // Is the character in bounds?
            if(bufferPosition < buffer.Length)
            {
                buffer[bufferPosition] = character;
            }
        }

        protected int GetBufferPosition(int x, int y)
        {
            return x + y * Console.BufferWidth;
        }
    }
}