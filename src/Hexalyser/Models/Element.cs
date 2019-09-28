
using System.Text;

namespace Hexalyser.Models
{
    /// <summary>
    /// Base class for all elements. This is also used for the raw hex dump elements.
    /// </summary>
    public class Element
    {
        public byte[] Bytes { get; protected set; }

        public string RtfText => ToRichText();

        public Element(byte[] bytes)
        {
            Bytes = bytes;
        }

        public virtual string ToRichText()
        {
            string s = "";
            int offset = 0;
            string line = "";
            string ascii = "";
            for (int i = 0; i < Bytes.Length; i++)
            {
                if (i != 0 && i % 16 == 0)
                {
                    s += $"{offset:x8}:{line,-50} {ascii}\n";
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
                    ascii += (char) Bytes[i];
                }
                else
                {
                    ascii += ".";
                }
            }

            if (line.Length != 0)
            {
                s += $"{offset:x8}:{line,-50}  {ascii}\n";
            }
            return s;
        }
    }
}
