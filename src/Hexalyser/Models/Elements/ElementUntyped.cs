namespace Hexalyser.Models.Elements
{
    public class ElementUntyped : Element
    {
        public ElementUntyped(byte[] bytes, Document document) : base(bytes, document)
        {
        }

        public override string ToText()
        {
            string s = $"<untyped count=\"{Bytes.Length}\">\n";
            int offset = 0;
            string line = "";
            string ascii = "";
            for (int i = 0; i < Bytes.Length; i++)
            {
                if (i != 0 && i % 16 == 0)
                {
                    s += $"    {offset:x8}:{line,-50} {ascii}\n";
                    offset += 16;
                    line = "";
                    ascii = "";
                }

                if (i % 8 == 0)
                {
                    line += " ";
                }
                line += $"{Bytes[i]:x2} ";

                if (Bytes[i] >= 0x20 && Bytes[i] < 0x7f)
                {
                    ascii += (char)Bytes[i];
                }
                else
                {
                    ascii += ".";
                }
            }

            if (line.Length != 0)
            {
                s += $"    {offset:x8}:{line,-50}  {ascii}\n";
            }
            s += $"</untyped>\n";
            return s;
        }

    }
}
