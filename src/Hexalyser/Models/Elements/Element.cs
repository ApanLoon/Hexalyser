using System;
using Hexalyser.Models.Expressions;

namespace Hexalyser.Models.Elements
{
    /// <summary>
    /// Base class for all elements. This is also used for the raw hex dump elements.
    /// </summary>
    public class Element
    {
        public int BytesPerItem { get; protected set; }

        public string TypeName { get; protected set; }
        public string Name { get; set; } = "";
        public Expression Count { get; set; } = new Expression("1");

        public Document Document { get; set; }
        public Element PreviousElement { get; set; }
        public Element NextElement { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }


        public string Text => ToText();

        public Element(Document document)
        {
            Document = document;
        }

        public virtual void Initialise(int offset, int length)
        {
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
            if (temp != null)
            {
                temp.PreviousElement = element;
            }
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

        /// <summary>
        /// Removes the element from the list. Note: If you remove the first element you might loose track of the entire list!
        /// </summary>
        public void Remove()
        {
            if (PreviousElement != null)
            {
                PreviousElement.NextElement = NextElement;
            }
            if (NextElement != null)
            {
                NextElement.PreviousElement = PreviousElement;
            }
            PreviousElement = null;
            NextElement = null;
        }

        public virtual string ToText()
        {
            throw new Exception("Don't call top level ToText() in Element.");
        }
    }
}
