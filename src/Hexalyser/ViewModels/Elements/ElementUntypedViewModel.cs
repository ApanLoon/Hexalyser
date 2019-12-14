using Hexalyser.Models.Elements;

namespace Hexalyser.ViewModels.Elements
{
    public class ElementUntypedViewModel : ElementViewModel
    {
        public string AddressesPropertyName = "Addresses";
        private string _addresses;
        public string Addresses
        {
            get => _addresses;
            set => Set(ref _addresses, value);
        }

        public string BytesPropertyName = "Bytes";
        private string _bytes;
        public string Bytes
        {
            get => _bytes;
            set => Set(ref _bytes, value);
        }

        public string AsciiPropertyName = "Ascii";
        private string _ascii;
        public string Ascii
        {
            get => _ascii;
            set => Set(ref _ascii, value);
        }

        public ElementUntypedViewModel(Element element) : base(element)
        {
            UpdateProperties();
        }

        protected byte[] Buffer => Element.Document.Buffer;

        protected void UpdateProperties()
        {
            int offset = 0;
            string addresses = "";
            string bytes = "";
            string ascii = "";
            int index = Element.Offset;
            for (int i = 0; i < Element.Length; i++)
            {
                if (i != 0 && i % 16 == 0)
                {
                    addresses += $"{offset:x8}\n";
                    offset += 16;
                    bytes += "\n";
                    ascii += "\n";
                }

                if (i % 16 != 0)
                {
                    bytes += " ";
                    if (i % 8 == 0)
                    {
                        bytes += " ";
                    }
                }
                bytes += $"{Buffer[index]:x2}";

                if (Buffer[index] >= 0x20 && Buffer[index] < 0x7f)
                {
                    ascii += (char)Buffer[index];
                }
                else
                {
                    ascii += ".";
                }
                index++;
            }

            Addresses = addresses;
            Bytes = bytes;
            Ascii = ascii;
        }
    }
}
