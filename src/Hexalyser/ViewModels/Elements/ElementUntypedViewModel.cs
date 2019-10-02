using Hexalyser.Extensions;
using Hexalyser.Models.Elements;

namespace Hexalyser.ViewModels.Elements
{
    public class ElementUntypedViewModel : ElementViewModel
    {
        public ElementUntypedViewModel(Element element) : base(element)
        {
        }

        public string AddressesPropertyName = "Addresses";
        public string Addresses
        {
            get
            {
                string s = "";
                for (int i = 0; i < Element.Bytes.Length; i += 16)
                {
                    s += $"{i:x8}:\n";
                }
                return s;
            }
        }

        public string BytesPropertyName = "Bytes";
        public string Bytes
        {
            get
            {
                string s = "";
                int lastLineLength = 0;
                for (int i = 0; i < Element.Bytes.Length; i++)
                {
                    lastLineLength += 3;
                    if (i > 0 && i % 8 == 0 && i % 16 != 0)
                    {
                        s += " ";
                        lastLineLength++;
                    }
                    if (i > 0 && i % 16 == 0)
                    {
                        s += "\n";
                        lastLineLength = 3;
                    }
                    s += $"{Element.Bytes[i]:x2} ";
                }

                s = s.AddCharacter(' ', 49 - lastLineLength);
                return s;
            }
        }

        public string AsciiPropertyName = "Ascii";
        public string Ascii
        {
            get
            {
                string s = "";
                for (int i = 0; i < Element.Bytes.Length; i++)
                {
                    if (Element.Bytes[i] >= 0x20 && Element.Bytes[i] < 0x7f)
                    {
                        s += (char)Element.Bytes[i];
                    }
                    else
                    {
                        s += ".";
                    }

                    if (i > 0 && i % 16 == 0)
                    {
                        s += "\n";
                    }
                }
                return s;
            }
        }
    }
}
