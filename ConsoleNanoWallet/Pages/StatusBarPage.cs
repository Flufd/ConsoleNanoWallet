using ConsoleNanoWallet.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleNanoWallet.Pages
{
    public class StatusBarPage : Page
    {
        private StatusBarComponent statusBarComponent;

        public StatusBarPage(WalletOptions walletOptions) : base(walletOptions)
        {
            statusBarComponent = new StatusBarComponent();
            components.Add(statusBarComponent);

        }

        private void CommunicationService_ReceivedExchangeRateEvent(object sender, WebsocketEvents.NanoEventArgs<WebsocketEvents.ExchangeRateEvent> e)
        {
            this.statusBarComponent.Status = $"${e.Value.Price}";
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
