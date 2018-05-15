using System;
namespace Wallet.Components
{
    public class TextBlockComponent : Component
    {
        public string Text { get; set; }
        public override void Render(char[] buffer, (ConsoleColor foreground, ConsoleColor background)[] colourBuffer, int screenWidth, Style style)
        {
            if(this.StyleOverride != null)
            {
                style = StyleOverride.Value;
            }


            byte[] v = System.Text.Encoding.UTF8.GetBytes(Text);
            var nanoText = Console.OutputEncoding.GetChars(v);

            var pos = (PositionX + PositionY * screenWidth) * 2;

            for (int i = 0; i < nanoText.Length; i++)
            {
                colourBuffer[(pos / 2) + i]  = (style.Foreground, style.Background);
            }

            Buffer.BlockCopy(nanoText, 0, buffer, pos, nanoText.Length * 2);
        }
    }
}