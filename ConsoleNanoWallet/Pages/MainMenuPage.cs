using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleNanoWallet.Pages
{
    public class MainMenuPage : Page
    {
        public MainMenuPage(WalletOptions walletOptions, CommunicationService communicationService) : base(walletOptions, communicationService)
        {
        }
    }
}
