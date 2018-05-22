using ConsoleNanoWallet.WebsocketEvents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleNanoWallet
{
    public class CommunicationService
    {
        public CommunicationService()
        {
            jsonParser = new JsonParser();
        }

        private ClientWebSocket webSocket;
        private readonly JsonParser jsonParser;

        public async Task Init()
        {
            while (true)
            {
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri($"wss://light.nano.org"), CancellationToken.None);

            WalletStartComplete?.Invoke(this, new EventArgs());

            try
            {
                await ReceiveData().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                    // TODO: log error or something
                break;
            }
            }
        }

        public async Task ReceiveData()
        {
            while (true)
            {
                ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[8192]);

                WebSocketReceiveResult result = null;

                using (var ms = new MemoryStream())
                {
                    do
                    {
                        result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    }
                    while (!result.EndOfMessage);

                    ms.Seek(0, SeekOrigin.Begin);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        using (var reader = new StreamReader(ms, Encoding.UTF8))
                        {
                            var json = await reader.ReadToEndAsync();
                            RaiseReceivedData(json);
                        }
                    }
                }
            }
        }

        private void RaiseReceivedData(string json)
        {
            var walletEvent = jsonParser.ParseEvent(json);

            if (walletEvent != null)
            {
                switch (walletEvent)
                {
                    case ExchangeRateEvent typedEvent:
                        ReceivedExchangeRateEvent?.Invoke(this, new NanoEventArgs<ExchangeRateEvent>(typedEvent));
                        break;
                    case AccountSummaryEvent typedEvent:
                        ReceivedAccountSummaryEvent?.Invoke(this, new NanoEventArgs<AccountSummaryEvent>(typedEvent));
                        break;
                    case AccountHistoryEvent typedEvent:
                        ReceivedAccountHistoryEvent?.Invoke(this, new NanoEventArgs<AccountHistoryEvent>(typedEvent));
                        break;
                    case WorkEvent typedEvent:
                        ReceivedWorkEvent?.Invoke(this, new NanoEventArgs<WorkEvent>(typedEvent));
                        break;
                    default:
                        break;
                }
            }
        }

        public event EventHandler<NanoEventArgs<ExchangeRateEvent>> ReceivedExchangeRateEvent;
        public event EventHandler<NanoEventArgs<AccountSummaryEvent>> ReceivedAccountSummaryEvent;
        public event EventHandler<NanoEventArgs<AccountHistoryEvent>> ReceivedAccountHistoryEvent;
        public event EventHandler<NanoEventArgs<WorkEvent>> ReceivedWorkEvent;
        public event EventHandler<EventArgs> WalletStartComplete;
    }
}
