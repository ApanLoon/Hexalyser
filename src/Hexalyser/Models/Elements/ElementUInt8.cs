using System.Collections;
using System.Collections.Generic;

namespace Hexalyser.Models.Elements
{
    public class ElementUInt8 : Element
    {
        public OffsetArray<byte> Values { get; protected set; }

        public ElementUInt8(Document document) : base(document)
        {
            TypeName = "UInt8";
            BytesPerItem = 1;
        }

        public override void Initialise(int offset, int length)
        {
            Offset = offset;
            Length = length;
            Count.Text = $"{length}";
            Values = new OffsetArray<byte>(Document.Buffer, offset, length);
        }

        public override string ToText()
        {
            string s = $"<{TypeName} count=\"1\">\n    ";
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
            s += $"</{TypeName}>\n";
            return s;
        }
    }
}
