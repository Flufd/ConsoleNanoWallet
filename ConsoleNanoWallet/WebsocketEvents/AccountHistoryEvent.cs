using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNanoWallet.WebsocketEvents
{
    public class AccountHistoryEvent : LightWalletEvent
    {
        public List<AccountHistoryItem> History { get; set; }
    }

    public class AccountHistoryWithPreviousEvent : AccountHistoryEvent
    {
        public string Previous { get; set; }
    }

    public class AccountHistoryItem
    {
        public string Account { get; set; }
        public BigInteger Amount { get; set; }
        public string Hash { get; set; }
        public TransactionType Type { get; set; }
    }

    public enum TransactionType
    {
        Send,
        Receive
    }
}
