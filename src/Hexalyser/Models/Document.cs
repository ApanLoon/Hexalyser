using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hexalyser.Models.Elements;

namespace Hexalyser.Models
{
    public class Document
    {
        #region Fields
        protected int ElementNameCount = 0;
        #endregion Fields

        #region Events
        public event Action<Document> SequenceChanged;
        protected void RaiseSequenceChanged()
        {
            SequenceChanged?.Invoke(this);
        }
        #endregion Events

        #region Properties
        public string Name { get; set; }
        public string FileName { get; set; }
        public int Length { get; protected set; }
        public byte[] Buffer { get; protected set; }

        public Element FirstElement { get; protected set; }

        public List<Element> Elements
        {
            get
            {
                List<Element> l = new List<Element>();
                Element e = FirstElement;
                while (e != null)
                {
                    l.Add(e);
                    e = e.NextElement;
                }
                return l;
            }
        }
        #endregion Properties

        #region Constructors
        public Document (string fileName)
        {
            try
            {
                FileName = fileName;
                Name = Path.GetFileName(fileName);

                Buffer = File.ReadAllBytes(fileName);
                Length = Buffer.Length;
                FirstElement = new ElementUntyped(this, 0, Length);
                FirstElement.Name = GetAutoName(this);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        public Document(string fileName, byte[] buf)
        {
            FileName = fileName;
            Name = Path.GetFileName(fileName);
            Length = buf.Length;
            FirstElement = new ElementUntyped(this, 0, Length);
        }
        #endregion Constructors

        public static Dictionary<string, Func<Element, int, Element>> InsertType = new Dictionary<string, Func<Element, int, Element>>()
        {
            {"uint16", (src, offset) => Insert<ElementUInt16>(src, offset, 2)},
            {"uint32", (src, offset) => Insert<ElementUInt32>(src, offset, 4)}
        };

        /// <summary>
        /// Inserts a new element into an ElementUntyped.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">The element to insert the new element into.</param>
        /// <param name="offset">In bytes, relative to the start of the src element.</param>
        /// <param name="length">In bytes of the new element.</param>
        /// <returns></returns>
        protected static Element Insert<T>(Element src, int offset, int length) where T : Element
        {
            // Don't call this on anything other than untyped elements:
            if (!(src is ElementUntyped))
            {
                return null;
            }

            Document document = src.Document;

            // Do we have enough bytes within the src element?
            if (src.Length < offset + length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), src.Length - offset,
                    $"There are not enough bytes in the buffer to insert type {typeof(T).Name}, {length} bytes are required.");
            }

            int bytesBefore = offset;
            int bytesMiddle = length;
            int bytesAfter  = src.Length - offset - length;

            int offsetMiddle = offset;
            int offsetAfter  = offset + length;

            Element newElement = (T)Activator.CreateInstance(typeof(T), document, src.Offset + offsetMiddle, bytesMiddle);
            newElement.Name = GetAutoName(document);

            if (offsetMiddle > 0 && bytesAfter > 0)
            {
                newElement.Append(new ElementUntyped(document, src.Offset + offsetAfter, bytesAfter) { Name = GetAutoName(document) });
                src.Length = bytesBefore;
                src.Append(newElement);
            }
            else if (offsetMiddle == 0 && bytesAfter > 0)
            {
                if (src.PreviousElement == null)
                {
                    document.FirstElement = newElement;
                }
                src.Offset = src.Offset + offsetAfter;
                src.Length = bytesAfter;
                src.Prepend(newElement);
            }
            else if (offsetMiddle > 0 && bytesAfter == 0)
            {
                src.Length = bytesBefore;
                src.Append(newElement);
            }
            else if (offsetMiddle == 0 && bytesAfter == 0)
            {
                if (src.PreviousElement == null)
                {
                    document.FirstElement = newElement;
                }
                newElement.Name = src.Name;
                src.Append(newElement);
                src.Remove();
            }

            document.RaiseSequenceChanged();
            return newElement;
        }

        protected static string GetAutoName(Document document)
        {
            return $"Element {document.ElementNameCount++}";
        }

        public void AppendElement(Element newElement, Element targetElement)
        {
            targetElement.Append(newElement);
            RaiseSequenceChanged();
        }

        public void PrependElement(Element newElement, Element targetElement)
        {
            targetElement.Prepend(newElement);
            RaiseSequenceChanged();
        }
    }
}
