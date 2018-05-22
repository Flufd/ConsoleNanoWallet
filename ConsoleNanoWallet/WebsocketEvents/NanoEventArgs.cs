using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleNanoWallet.WebsocketEvents
{
    public class NanoEventArgs<T> : EventArgs
    {
        public NanoEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}

