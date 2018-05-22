using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ConsoleNanoWallet.WebsocketEvents
{
    public class AccountSummaryEvent : LightWalletEvent
    {
        public BigInteger Balance { get; set; }
        public int BlockCount { get; set; }
        public decimal BTC { get; set; }
        public string Currency { get; set; }
        public string Frontier { get; set; }
        public long ModifiedTimestamp { get; set; }
        public string OpenBlock { get; set; }
        public BigInteger Pending { get; set; }
        public decimal Price { get; set; }
        public string Representative { get; set; }
        public string RepresentativeBlock { get; set; }
        public Guid Uuid { get; set; }
    }
}
