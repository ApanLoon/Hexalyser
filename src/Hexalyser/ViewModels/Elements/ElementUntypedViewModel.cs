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
                return s.Substring(0, s.Length - 1); // Drop the last newline
            }
        }

        public string BytesPropertyName = "Bytes";
        public string Bytes
        {
            get
            {
                string s = "";
                for (int i = 0; i < Element.Bytes.Length; i++)
                {
                    if (i > 0 && i % 16 != 0)
                    {
                        s += " ";
                    }
                    if (i > 0 && i % 8 == 0 && i % 16 != 0)
                    {
                        s += " ";
                    }
                    if (i > 0 && i % 16 == 0)
                    {
                        s += "\n";
                    }
                    s += $"{Element.Bytes[i]:x2}";
                }
                return s.Trim();
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
                return s.Trim();
            }
        }
    }
}
