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
                    PositionX = 2,
                    PositionY = 4,
                    StyleOverride = new Style(ConsoleColor.Black , ConsoleColor.White)
                });
                
                while (true)
                {   
                    RenderComponents();

                    DrawBuffer();

                    Thread.Sleep(30);
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
            Console.BackgroundColor = walletOptions.Style.Background;
            Console.ForegroundColor = walletOptions.Style.Accent;

            for (int i = 0; i < buffer.Length; i++)
            {
                Console.ForegroundColor = buffer[i].Style.Foreground;
                Console.BackgroundColor = buffer[i].Style.Background;
                Console.Out.Write(buffer[i].Character);
            }
        }
    }
}