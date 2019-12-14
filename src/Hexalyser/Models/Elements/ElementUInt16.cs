using System;

namespace Hexalyser.Models.Elements
{
    public class ElementUInt16 : Element
    {
        public UInt16[] Values { get; set; }

        public ElementUInt16(Document document, int offset, int length) : base(document, offset, length)
        {
            TypeName = "UInt16";
            Count.Text = $"{length / 2}";
            UpdateValues();
        }

        private void UpdateValues()
        {
            int count = Count.Evaluate();
            Values = new UInt16[count];
            for (int i = 0; i < count; i++)
            {
                Values[i] = BitConverter.ToUInt16(Document.Buffer, Offset + i * 2);
            }
        }

        public override string ToText()
        {
            string s = $"<uint16 count=\"1\">\n    ";
            int count = Count.Evaluate();
            for (int i = 0; i < count; i++)
            {
                s += $"{Values[i]}";
                if (i < count - 1)
                {
                    s += ", ";
                }

                if (i > 0 && i % 8 == 0)
                {
                    s += "\n    ";
                }
            }
            s += $"</uint16>\n";
            return s;
        }
    }
}