using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Wallet.Components;
using QRCoder;

namespace Wallet
{
    public class Page : IPage
    {
        private List<Component> components = new List<Component>();
        private readonly WalletOptions walletOptions;
      
        public char[] buffer;
        private (ConsoleColor foreground, ConsoleColor background)[] colourBuffer;

        public Page(WalletOptions walletOptions)
        {
            this.walletOptions = walletOptions;
        }

        public virtual async Task<object> RunAsync()
        {
            await Task.Run(() =>
            {
                //Console.BufferWidth = 200;
                Console.BufferWidth = Console.WindowWidth;
                var i = 1;
                Console.BufferHeight = Console.WindowHeight;
                Console.CursorVisible = false;
                // Console.OutputEncoding = System.Text.Encoding.UTF8;
              
                buffer = new char[Console.WindowWidth * (Console.WindowHeight - 1)];
                colourBuffer = new (ConsoleColor, ConsoleColor)[Console.WindowWidth * (Console.WindowHeight - 1)];
                for (int j = 0; j < buffer.Length; j++)
                {
                    this.buffer[j] = ' ';
                    this.colourBuffer[j] = (this.walletOptions.Style.Foreground, this.walletOptions.Style.Background);
                }

                Console.BackgroundColor = this.walletOptions.Style.Background;
                Console.Clear();
                var text = "";

                components.Add(new TextBlockComponent() { Text = "NANO Console Wallet", PositionX = 5, PositionY = 5});
                components.Add(new TextBlockComponent() 
                { 
                    Text = "Custom colour", 
                    PositionX = 5, 
                    PositionY = 6, 
                    StyleOverride = new Style{
                        Background = ConsoleColor.Red,
                        Foreground = ConsoleColor.Black
                    }
                });
                
                while (true)
                {
                    RenderComponents();
                    // Update the element positions
                    //Console.WriteLine(Console.WindowLeft);
                    //Console.BufferHeight = Console.WindowHeight;
                    var widthPerPercent = Console.BufferWidth / 100f;
                    var progressWidth = (int)Math.Floor(widthPerPercent * (i % 100));

                    // buffer = new String('█', progressWidth);
                    // buffer += new String(' ', Console.BufferWidth - progressWidth);
                    // cbuffer[0] = (i % 10).ToString()[0];
                    // cbuffer[1] = '█';

                    // buffer =  new String(cbuffer);
                    // if(Console.KeyAvailable)
                    // {
                    //     text += Console.ReadKey(true).KeyChar;
                    // }                    
                    // for (int k = 0; k < text.Length; k++)
                    // {
                    //     cbuffer[2 + k] = text[k];
                    // }

                    DrawBuffer();
                    Thread.Sleep(30);
                    i++;
                }
            });

            return null;
        }

        public void RenderComponents()
        {
            foreach (var component in components)
            {
                component.Render(buffer, colourBuffer, Console.BufferWidth, this.walletOptions.Style);
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.L);
            AsciiQRCode qrCode = new AsciiQRCode(qrCodeData);
           var lines = qrCode.GetLineByLineGraphic(1);
            for (int r = 0; r < lines.Length; r++)
            {
                 buffer[r * Console.BufferWidth] = r.ToString()[0];
                 for (int j = 0; j < lines[r].Length; j+=2)
                 {
                     if(lines[r][j] == '█')
                     {
                         if(lines.Length > r+1 && lines[r+1][j] == '█')
                         {
                             buffer[r/2 *Console.BufferWidth + j/2] = '█';
                         }
                         else{
                             buffer[r/2*Console.BufferWidth + j/2] = '▀'; 
                         }
                     }
                     if(lines[r][j] == ' ')
                     {
                         if(lines.Length > r+1 && lines[r+1][j] == ' ')
                         {
                             buffer[r/2 *Console.BufferWidth + j/2] = ' ';
                         }
                         else{
                             buffer[r/2*Console.BufferWidth + j/2] = '▄'; 
                         }
                     }
                 }
                 r++;

            }
            // string qrCodeAsAsciiArt = qrCode.GetGraphic(1);//.Replace("\n","");
            // for (int i = 0; i < qrCodeAsAsciiArt.Length; i++)
            // {           
            //     if(10*80 + i < buffer.Length)  {

            //     buffer[i] = '▀';//qrCodeAsAsciiArt[i];
            //     }
            // }
        }

        public void DrawBuffer()
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = this.walletOptions.Style.Background;
            Console.ForegroundColor = this.walletOptions.Style.Accent;

            for (int i = 0; i < buffer.Length; i++)
            {
                Console.ForegroundColor = colourBuffer[i].foreground;
                Console.BackgroundColor = colourBuffer[i].background;
                Console.Out.Write(buffer[i]);
            }

            //Console.Write(buffer);
            //Console.Out.Write(buffer);
        }
    }
}