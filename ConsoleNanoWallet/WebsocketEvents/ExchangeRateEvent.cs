using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNanoWallet.WebsocketEvents
{
    public class ExchangeRateEvent : LightWalletEvent
    {
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public decimal BTC { get; set; }
    }
}
