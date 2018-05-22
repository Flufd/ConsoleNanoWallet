using ConsoleNanoWallet.Pages;
using System;
using System.Threading.Tasks;
namespace ConsoleNanoWallet
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new WalletOptions
            {
                Style = new Style
                {
                    Foreground = ConsoleColor.Blue,
                    Background = ConsoleColor.White,
                    AccentBackground = ConsoleColor.DarkBlue,
                    AccentForeground = ConsoleColor.White
                }
            };
            var communicationService = new CommunicationService();

            using (var page = new AccountPage(options, communicationService))
            {
                Task.Run(communicationService.Init);
                await page.RunAsync();
            }
        }


    }
}
