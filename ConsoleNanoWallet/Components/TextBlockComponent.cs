using ConsoleNanoWallet.Rendering;
using System;
namespace ConsoleNanoWallet.Components
{
    public class TextBlockComponent : Component
    {
        public string Text { get; set; } = "";
        public override void Render(StyledCharacter[] buffer, Style style)
        {
            if(this.StyleOverride != null)
            {
                style = StyleOverride.Value;
            }
            
            byte[] v = System.Text.Encoding.UTF8.GetBytes(Text);
            var nanoText = Console.OutputEncoding.GetChars(v);

            var pos = (PositionX + PositionY * Console.BufferWidth) * 2;

            for (int i = 0; i < nanoText.Length; i++)
            {
                buffer[(pos / 2) + i] = new StyledCharacter(nanoText[i], style);
            }
        }
    }
}