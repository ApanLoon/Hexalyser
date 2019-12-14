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

        public int SelectionStartRaw
        {
            get => _selectionStartRaw;
            set
            {
                Set(ref _selectionStartRaw, value);
                int row = _selectionStartRaw / 49;
                int col = (int)(0.5f + (_selectionStartRaw % 49) / 3f);
                int index = row * 16 + col;
                SelectionStart = index;
            } 
        }
        private int _selectionStartRaw;

        public int SelectionLengthRaw
        {
            get => _selectionLengthRaw;
            set
            {
                Set(ref _selectionLengthRaw, value);
                int row = _selectionLengthRaw / 49;
                int col = (int)(0.5f + (_selectionLengthRaw % 49) / 3f);
                int index = row * 16 + col;
                SelectionLength = index;
            }
        }
        private int _selectionLengthRaw;


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
