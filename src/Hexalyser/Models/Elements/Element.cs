using System;
using Hexalyser.Models.Expressions;

namespace Hexalyser.Models.Elements
{
    /// <summary>
    /// Base class for all elements. This is also used for the raw hex dump elements.
    /// </summary>
    public class Element
    {
        public string TypeName { get; protected set; }
        public string Name { get; set; } = "";
        public Expression Count { get; set; } = new Expression("1");

        public Document Document { get; set; }
        public Element PreviousElement { get; set; }
        public Element NextElement { get; set; }
        public byte[] Bytes { get; set; }

        public string Text => ToText();

        public Element(byte[] bytes, Document document)
        {
            Bytes = bytes;
            Document = document;
        }

        /// <summary>
        /// Appends a sequence of elements to this element.
        /// </summary>
        /// <param name="element"></param>
        public void Append(Element element)
        {
            Element temp = NextElement;
            NextElement = element;
            element.PreviousElement = this;
            while (element.NextElement != null)
            {
                element = element.NextElement;
            }
            element.NextElement = temp;
        }

        /// <summary>
        /// Prepends a sequence of elements to this element.
        /// </summary>
        /// <param name="element"></param>
        public void Prepend(Element element)
        {
            element.PreviousElement = PreviousElement;
            PreviousElement.NextElement = element;
            while (element.NextElement != null)
            {
                element = element.NextElement;
            }
            PreviousElement = element;
            element.NextElement = this;
        }

        public virtual string ToText()
        {
            throw new Exception("Don't call top level ToText() in Element.");
        }
    }
}
