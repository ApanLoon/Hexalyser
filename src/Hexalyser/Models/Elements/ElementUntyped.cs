﻿namespace Hexalyser.Models.Elements
{
    public class ElementUntyped : Element
    {
        public ElementUntyped(Document document) : base(document)
        {
            TypeName = "Untyped";
        }

        public override void Initialise(int offset, int length)
        {
            Offset = offset;
            Length = length;
            Count.Text = $"{length}";
        }

        public override string ToText()
        {
            string s = $"<{TypeName} count=\"{Document.Buffer.Length}\">\n";
            int offset = 0;
            string line = "";
            string ascii = "";
            for (int i = 0; i < Document.Length; i++)
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
                line += $"{Document.Buffer[i]:x2} ";

                if (Document.Buffer[i] >= 0x20 && Document.Buffer[i] < 0x7f)
                {
                    ascii += (char)Document.Buffer[i];
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
            s += $"</{TypeName}>\n";
            return s;
        }

    }
}
