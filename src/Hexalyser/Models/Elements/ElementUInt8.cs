using System.Collections;
using System.Collections.Generic;

namespace Hexalyser.Models.Elements
{
    public class ElementUInt8 : Element
    {
        public OffsetArray<byte> Values { get; protected set; }

        public ElementUInt8(Document document, int offset, int length) : base(document, offset, length)
        {
            TypeName = "UInt8";
            Count.Text = $"{length}";
            Values = new OffsetArray<byte>(document.Buffer, offset, length);
        }

        public override string ToText()
        {
            string s = $"<uint8 count=\"1\">\n    ";
            int count = Count.Evaluate();
            for (int i = 0; i < count; i++)
            {
                s += $"{Document.Buffer[i]}";
                if (i < count - 1)
                {
                    s += ", ";
                }

                if (i > 0 && i % 8 == 0)
                {
                    s += "\n    ";
                }
            }
            s += $"</uint8>\n";
            return s;
        }
    }
}
