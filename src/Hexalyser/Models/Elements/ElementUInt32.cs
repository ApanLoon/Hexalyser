
using System;

namespace Hexalyser.Models.Elements
{
    public class ElementUInt32 : Element
    {
        public UInt32[] Values { get; set; }

        public ElementUInt32(byte[] bytes, Document document) : base(bytes, document)
        {
            TypeName = "UInt32";
            UpdateValues();
        }

        private void UpdateValues()
        {
            int count = Count.Evaluate();
            Values = new UInt32[count];
            for (int i = 0; i < count; i++)
            {
                Values[i] = BitConverter.ToUInt32(Bytes, i * 4);
            }
        }

        public override string ToText()
        {
            string s = $"<{TypeName} count=\"{Count}\">\n    ";
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
            s += $"</{TypeName}>\n";
            return s;
        }
    }
}
