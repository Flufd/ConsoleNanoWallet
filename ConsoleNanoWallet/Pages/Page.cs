using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QRCoder;
using ConsoleNanoWallet.Components;
using ConsoleNanoWallet.Rendering;
using ConsoleNanoWallet.WebsocketEvents;

namespace ConsoleNanoWallet.Pages
{
    public class Page : IPage, IDisposable
    {
        protected List<Component> components = new List<Component>();
        protected readonly WalletOptions walletOptions;
        public StyledCharacter[] buffer;
        protected CancellationToken cancellationToken;

        public Page(WalletOptions walletOptions)
        {
            this.walletOptions = walletOptions;
        }

        public virtual async Task<object> RunAsync()
        {
            //await Task.Run(() =>
            //{
            var drawCount = 0;

            // Init screen
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            Console.CursorVisible = false;
            buffer = new StyledCharacter[Console.WindowWidth * (Console.WindowHeight)];


            Console.BackgroundColor = walletOptions.Style.AccentBackground;
            Console.Clear();

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                // Run the logic for the page
                await RunPageLogic();
                // Update the draw buffer to the size of the console
                buffer = new StyledCharacter[Console.WindowWidth * (Console.WindowHeight)];
                // Initialize buffer
                for (int j = 0; j < buffer.Length; j++)
                {
                    buffer[j] = new StyledCharacter(' ', walletOptions.Style.Foreground, walletOptions.Style.Background);
                }

                RenderComponents();

                DrawBuffer();

                Thread.Sleep(10);
                drawCount++;

            }
            //});

            return null;
        }

        public virtual async Task<object> RunPageLogic()
        {
            return await Task.FromResult(0);

        }

        public virtual void RenderComponents()
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
            Console.ForegroundColor = walletOptions.Style.AccentBackground;

            var currenctDrawindex = 0;
            var drawStrings = new List<(string content, Style style)>();

            var current = (content: "", walletOptions.Style);
            for (int i = 0; i < buffer.Length; i++)
            {
                if (current.Style == buffer[i].Style)
                {
                    current.content += buffer[i].Character;
                }
                else
                {
                    drawStrings.Add(current);
                    current = (content: "" + buffer[i].Character, buffer[i].Style);
                }

            }
            drawStrings.Add(current);

            foreach (var (content, style) in drawStrings)
            {
                // Don't draw on teh last character, this will cause the window to scroll down
                if (currenctDrawindex + content.Length < (Console.WindowHeight * Console.WindowWidth))
                {
                    Console.ForegroundColor = style.Foreground;
                    Console.BackgroundColor = style.Background;
                    Console.Out.Write(content);
                }
                else
                {
                    Console.ForegroundColor = style.Foreground;
                    Console.BackgroundColor = style.Background;
                    Console.Out.Write(content.Substring(0, content.Length - 1));
                    break;
                }
                currenctDrawindex += content.Length;
            }
        }

        public virtual void Dispose()
        {
           
        }
    }
}