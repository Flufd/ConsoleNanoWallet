using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleNanoWallet.Pages
{
    public class AccountPage : StatusBarPage
    {
        public AccountPage(WalletOptions walletOptions, CommunicationService communicationService) : base(walletOptions, communicationService)
        {
        }

        public override async Task<object> RunPageLogic()
        {
            if (Console.KeyAvailable)
            {
                if(Console.ReadKey().Key == ConsoleKey.A)
                {
                    using(var t = new TestPage(walletOptions, communicationService))
                    {
                        await t.RunAsync();
                    }
                }
                else
                {
                this.cancellationToken = new CancellationToken(true);

                }
            }

            return await Task.FromResult(0);
            
        }
    }
}
