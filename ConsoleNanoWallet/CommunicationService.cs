using ConsoleNanoWallet.WebsocketEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleNanoWallet
{
    public class CommunicationService
    {
        public CommunicationService(string address)
        {
            jsonParser = new JsonParser();
            this.Address = address;
        }

        private ClientWebSocket webSocket;
        private readonly JsonParser jsonParser;

        public string Address { get; set; }

        public async Task Init()
        {
            while (true)
            {
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri($"wss://light.nano.org"), CancellationToken.None);

                WalletStartComplete?.Invoke(this, new EventArgs());

                var json = JsonConvert.SerializeObject(new { account = this.Address, action = "account_subscribe", currency = "USD" }, Formatting.None);
                var bytes = Encoding.UTF8.GetBytes(json);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

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
                    case ExchangeRateEvent exchangeRateEvent:
                        ReceivedExchangeRateEvent?.Invoke(this, new NanoEventArgs<ExchangeRateEvent>(exchangeRateEvent));
                        break;
                    case AccountSummaryEvent accountSummaryEvent:
                        ReceivedAccountSummaryEvent?.Invoke(this, new NanoEventArgs<AccountSummaryEvent>(accountSummaryEvent));
                        break;
                    case AccountHistoryEvent accountHistoryEvent:
                        ReceivedAccountHistoryEvent?.Invoke(this, new NanoEventArgs<AccountHistoryEvent>(accountHistoryEvent));
                        break;
                    case WorkEvent workEvent:
                        ReceivedWorkEvent?.Invoke(this, new NanoEventArgs<WorkEvent>(workEvent));
                        break;
                    case BlockEvent blockEvent:
                        ReceivedBlockEvent?.Invoke(this, new NanoEventArgs<BlockEvent>(blockEvent));
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
        public event EventHandler<NanoEventArgs<BlockEvent>> ReceivedBlockEvent;
        public event EventHandler<EventArgs> WalletStartComplete;
    }
}
