using System;

namespace Hexalyser.Models.Elements
{
    public class ElementUInt16 : Element
    {
        public UInt16 Value { get; set; }

        public ElementUInt16(byte[] bytes, Document document) : base(bytes, document)
        {
            Value = BitConverter.ToUInt16(bytes);
        }

        public override string ToText()
        {
            string s = $"<uint16 count=\"1\">\n";
            s += $"    {Value}\n";
            s += $"</uint16>\n";
            return s;
        }
    }
}