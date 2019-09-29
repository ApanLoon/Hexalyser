
using System;

namespace Hexalyser.Models.Elements
{
    public class ElementUInt32 : Element
    {
        public UInt32 Value { get; set; }

        public ElementUInt32(byte[] bytes, Document document) : base(bytes, document)
        {
            Value = BitConverter.ToUInt32(bytes);
        }

        public override string ToText()
        {
            string s = $"<uint32 count=\"1\">\n";
            s += $"    {Value}\n";
            s += $"</uint32>\n";
            return s;
        }
    }
}
