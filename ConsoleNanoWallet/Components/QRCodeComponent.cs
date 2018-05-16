using ConsoleNanoWallet.Rendering;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleNanoWallet.Components
{
    public class QRCodeComponent : Component
    {
        private QRCodeGenerator qrGenerator;

        private string content;
        private QRCodeData qrCodeData;

        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                using(var qrGenerator = new QRCodeGenerator())
                {
                    qrCodeData = qrGenerator.CreateQrCode(Content ?? "", QRCodeGenerator.ECCLevel.M);
                }
            }
        }


        public override void Render(StyledCharacter[] buffer, Style style)
        {
            // We have a 2d array of bits, we can render them with the characters █ ▀ ▄ and space by reading them two rows at a time
            
            // Loop rows
            for (int y = 0; y < qrCodeData.ModuleMatrix.Count; y++)
            {
                // Loop columns
                for (int x = 0; x < qrCodeData.ModuleMatrix[y].Length; x++)
                {
                    var topValue = qrCodeData.ModuleMatrix[y][x];
                    var bottomValue = qrCodeData.ModuleMatrix.Count > y + 1 && qrCodeData.ModuleMatrix[y + 1][x];
                    // If the top row is 1
                    if (topValue)
                    {
                        // If the bottom row is 1
                        if (bottomValue)
                        {
                            RenderLocalCharacter(buffer, new StyledCharacter('█', style), x, y / 2);
                        }
                        else // Bottom row is 0
                        {
                            RenderLocalCharacter(buffer, new StyledCharacter('▀', style), x, y / 2);
                        }
                    }
                    else
                    {
                        // If the bottom row is 1
                        if (bottomValue)
                        {
                            RenderLocalCharacter(buffer, new StyledCharacter('▄', style), x, y / 2);
                        }
                        else // Bottom row is 0
                        {
                            RenderLocalCharacter(buffer, new StyledCharacter(' ', style), x, y / 2);
                        }
                    }
                }

                // Skip next row as we have just rendered 2
                y++;

            }
        }

    }
}
