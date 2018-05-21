using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QRCoder;
using ConsoleNanoWallet.Components;
using ConsoleNanoWallet.Rendering;

namespace ConsoleNanoWallet
{
    public class Page : IPage
    {
        private List<Component> components = new List<Component>();
        private readonly WalletOptions walletOptions;
      
        public StyledCharacter[] buffer;

        public Page(WalletOptions walletOptions)
        {
            this.walletOptions = walletOptions;
        }

        public virtual async Task<object> RunAsync()
        {
            await Task.Run(() =>
            {
                var drawCount = 0;

                // Init screen
                Console.BufferWidth = Console.WindowWidth;
                Console.BufferHeight = Console.WindowHeight;
                Console.CursorVisible = false;
                buffer = new StyledCharacter[Console.WindowWidth * (Console.WindowHeight - 1)];
              
                // Initialize buffer
                for (int j = 0; j < buffer.Length; j++)
                {
                    this.buffer[j] = new StyledCharacter(' ', walletOptions.Style.Foreground, walletOptions.Style.Background) ;
                }

                Console.BackgroundColor = this.walletOptions.Style.Background;
                Console.Clear();             
                
                // Add some test components
                components.Add(new TextBlockComponent() { Text = "NANO Console Wallet", PositionX = 2, PositionY = 2});
               

                components.Add(new QRCodeComponent
                {
                    Content = "Hi",
                    PositionX = 20,
                    PositionY = 4,
                    StyleOverride = new Style(ConsoleColor.Red , ConsoleColor.White)
                });
                
                while (true)
                {
                    // Update the draw buffer to the size of the console
                    buffer = new StyledCharacter[Console.WindowWidth * (Console.WindowHeight - 1)];
                    // Initialize buffer
                    for (int j = 0; j < buffer.Length; j++)
                    {
                        this.buffer[j] = new StyledCharacter(' ', walletOptions.Style.Foreground, walletOptions.Style.Background);
                    }

                    RenderComponents();

                    DrawBuffer();

                    Thread.Sleep(10);
                    drawCount++;
                }
            });

            return null;
        }

        public void RenderComponents()
        {
            foreach (var component in components)
            {
                component.Render(buffer, walletOptions.Style);
            }
        }

        public void DrawBuffer()
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Console.BackgroundColor = walletOptions.Style.Background;
            Console.ForegroundColor = walletOptions.Style.Accent;

            var drawStrings = new List<(string content, Style style)>();

            var current = (content: "", walletOptions.Style);
            for (int i = 0; i < buffer.Length; i++)
            {
                if(current.Style == buffer[i].Style)
                {
                    current.content += buffer[i].Character;
                    //Console.ForegroundColor = buffer[i].Style.Foreground;
                    //Console.BackgroundColor = buffer[i].Style.Background;
                    //Console.Out.Write(buffer[i].Character);
                }
                else
                {
                    drawStrings.Add(current);
                    current = (content: "" + buffer[i].Character, buffer[i].Style);
                }

            }
            drawStrings.Add(current);

            foreach (var drawString in drawStrings)
            {
                Console.ForegroundColor = drawString.style.Foreground;
                Console.BackgroundColor = drawString.style.Background;
                Console.Out.Write(drawString.content);
            }
        }
    }
}