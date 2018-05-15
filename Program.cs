﻿using System;
using System.Threading.Tasks;
namespace Wallet
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new WalletOptions
            {
                Style = new Style { Foreground = ConsoleColor.Blue, Background = ConsoleColor.White, Accent = ConsoleColor.DarkBlue }
            };
            var page = new Page(options);
            await page.RunAsync();
            Console.WriteLine("Hello World!");
        }
    }
}
