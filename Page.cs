using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wallet
{
    public class Page : IPage
    {
        private readonly WalletOptions walletOptions;
        private string buffer;
        public char[] cbuffer;

        public Page(WalletOptions walletOptions)
        {
            this.walletOptions = walletOptions;
        }

        public virtual async Task<object> RunAsync()
        {
            await Task.Run(()=>
            {
                //Console.BufferWidth = 200;
                Console.BufferWidth = Console.WindowWidth;
                var i = 1;
                Console.BufferHeight = Console.WindowHeight;
                Console.CursorVisible = false;
               // Console.OutputEncoding = System.Text.Encoding.UTF8;
                this.buffer = "";
                this.cbuffer = new char[Console.WindowWidth * (Console.WindowHeight-1)];
                for (int j = 0; j < Console.WindowWidth *  (Console.WindowHeight-1); j++)
                {
                    this.cbuffer[j] = ' ';
                }

                Console.BackgroundColor = this.walletOptions.Style.Background;
                Console.Clear();
                var text = "";

                while(true)
                {
                    // Update the element positions
                    //Console.WriteLine(Console.WindowLeft);
                    //Console.BufferHeight = Console.WindowHeight;
                    var widthPerPercent = Console.BufferWidth / 100f;                    
                    var progressWidth = (int)Math.Floor(widthPerPercent * (i % 100));                    

                    buffer =  new String('█', progressWidth);
                    buffer += new String(' ', Console.BufferWidth - progressWidth);
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

        public void DrawBuffer()
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = this.walletOptions.Style.Background;
            Console.ForegroundColor = this.walletOptions.Style.Accent;

           
                //Console.Write(buffer);
           Console.Out.Write(buffer);
        }
    }
}