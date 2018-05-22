using ConsoleNanoWallet.Components;
using NanoDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNanoWallet.Pages
{
    public class MainMenuPage : Page
    {
        private List<temp> accounts;
        private TextBlockComponent x;
        int a = 0;
        public MainMenuPage(WalletOptions walletOptions) : base(walletOptions)
        {
            x = new TextBlockComponent();
            components.Add(x);
            accounts = new List<temp>();

            components.Add(new ListViewComponent<temp>(accounts, (x) => $"{ ShortAddress(x.Account)}|{x.Amount.ToString(AmountBase.Nano)}") { PositionY = 2 });
                      
            accounts.Add(new temp
            {
                Account = "xrb_31337cwbxe6and3zo68qn4p1aeb94hz38eyctsaxib6tsp4awrux8ieod59o",
                Amount = new NanoAmount(),
                CommunicationService = new CommunicationService("xrb_31337cwbxe6and3zo68qn4p1aeb94hz38eyctsaxib6tsp4awrux8ieod59o")
            });

            accounts.Add(new temp
            {
                Account = "xrb_3nano9ho4prfx13kimceuuomdehtqg175c153x1opst7kxt77g4eew9o3ji9",
                Amount = new NanoAmount(),
                CommunicationService = new CommunicationService("xrb_3nano9ho4prfx13kimceuuomdehtqg175c153x1opst7kxt77g4eew9o3ji9")
            });

            accounts.Add(new temp
            {
                Account = "xrb_3iuommeb5gpnhxbsubeswmbg5rdydfstzpih9qgmmfcnqmxs1bzeootdqryq",
                Amount = new NanoAmount(),
                CommunicationService = new CommunicationService("xrb_3iuommeb5gpnhxbsubeswmbg5rdydfstzpih9qgmmfcnqmxs1bzeootdqryq")
            });


            foreach (var account in accounts)
            {
                account.CommunicationService.ReceivedBlockEvent += CommunicationService_ReceivedBlockEvent;
                Task.Run(account.CommunicationService.Init);
            }

        }

        private void CommunicationService_ReceivedBlockEvent(object sender, WebsocketEvents.NanoEventArgs<WebsocketEvents.BlockEvent> e)
        {
            var acc = accounts.FirstOrDefault(a => a.Account == e.Value.Block.Account);
            if (acc != null)
            {
                acc.Amount = new NanoAmount(e.Value.Block.Balance, AmountBase.raw);
            }
        }

        public string ShortAddress(string address) => address.Substring(0, 9) + ".." + address.Substring(address.Length - 5, 5);

        private class temp
        {
            public string Account { get; set; }
            public NanoAmount Amount { get; set; }
            public CommunicationService CommunicationService { get; set; }
        }
    }
}
