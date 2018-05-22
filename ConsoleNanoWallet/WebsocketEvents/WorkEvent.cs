using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNanoWallet.WebsocketEvents
{
    public class WorkEvent : LightWalletEvent
    {
        public string Work { get; set; }
    }
}
