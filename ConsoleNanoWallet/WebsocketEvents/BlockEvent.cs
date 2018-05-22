using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ConsoleNanoWallet.WebsocketEvents
{
    public class BlockEvent : LightWalletEvent
    {
        public string Account { get; set; }
        public string Hash { get; set; }
        public BigInteger Amount { get; set; }
        public bool IsSend { get; set; }
        public Block Block { get; set; }
    }

    public class Block
    {
        public string Type { get; set; }
        public string Account { get; set; }
        public string Previous { get; set; }
        public string Representative { get; set; }
        public BigInteger Balance { get; set; }
        public string Link { get; set; }
        public string LinkAsAccount { get; set; }
        public string Signature { get; set; }
    }
}
