using System;
using System.Collections.Generic;
using System.Text;
using ConsoleNanoWallet.Rendering;

namespace ConsoleNanoWallet.Components
{
    public class ListViewComponent<T> : Component
    {
        private readonly List<T> list;
        private readonly Func<T, string> formatter;

        public ListViewComponent(List<T> list, Func<T, string> formatter)
        {
            this.list = list;
            this.formatter = formatter;
        }

        public override void Render(StyledCharacter[] buffer, Style style)
        {
            var row = 0;
            foreach (var item in list)
            {
                var formattedString = formatter(item);
                for (int i = 0; i < formattedString.Length; i++)
                {
                    RenderLocalCharacter(buffer, new StyledCharacter(formattedString[i], style), i, row);
                }
                row++;
            }
        }
    }
}
