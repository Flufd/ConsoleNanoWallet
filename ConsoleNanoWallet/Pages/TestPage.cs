using ConsoleNanoWallet.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleNanoWallet.Pages
{
    public class TestPage : Page
    {
        public TestPage(WalletOptions walletOptions, CommunicationService communicationService) : base(walletOptions, communicationService)
        {
            components.Add(new TextBlockComponent
            {
                PositionX = 5,
                PositionY = 5,
                Text = "test"
            });
        }

        public async override Task<object> RunPageLogic()
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey();
                
                    this.cancellationToken = new CancellationToken(true);

                
            }

            return await Task.FromResult(0);
        }
    }
}
