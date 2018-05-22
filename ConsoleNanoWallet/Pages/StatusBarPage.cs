using ConsoleNanoWallet.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleNanoWallet.Pages
{
    public class StatusBarPage : Page
    {
        private StatusBarComponent statusBarComponent;

        public StatusBarPage(WalletOptions walletOptions, CommunicationService communicationService) : base(walletOptions, communicationService)
        {
            statusBarComponent = new StatusBarComponent();
            components.Add(statusBarComponent);

            this.communicationService.ReceivedExchangeRateEvent += CommunicationService_ReceivedExchangeRateEvent;
        }

        private void CommunicationService_ReceivedExchangeRateEvent(object sender, WebsocketEvents.NanoEventArgs<WebsocketEvents.ExchangeRateEvent> e)
        {
            this.statusBarComponent.Status = $"${e.Value.Price}";
        }

        public override void Dispose()
        {
            base.Dispose();
            this.communicationService.ReceivedExchangeRateEvent -= CommunicationService_ReceivedExchangeRateEvent;
        }
    }
}
