using System;
using System.Collections.Generic;
using System.IO;
using Hexalyser.Models.Elements;

namespace Hexalyser.Models
{
    public class Document
    {
        #region Fields
        protected int ElementNameCount = 0; // Used for name generation
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
                FirstElement = new ElementUntyped(this);
                FirstElement.Initialise(0, Length);
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
            FirstElement = new ElementUntyped(this);
            FirstElement.Initialise(0, Length);
        }
        #endregion Constructors

        public static Dictionary<string, Func<Element, int, int, Element>> InsertType = new Dictionary<string, Func<Element, int, int, Element>>()
        {
            {"uint8",  Insert<ElementUInt8>},
            {"uint16", Insert<ElementUInt16>},
            {"uint32", Insert<ElementUInt32>}
        };

        /// <summary>
        /// Inserts a new element into an ElementUntyped.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">The element to insert the new element into.</param>
        /// <param name="offset">In bytes, relative to the start of the src element.</param>
        /// <param name="length">In bytes of the new element. This will be rounded down to the largest number of complete items that fit.</param>
        /// <returns></returns>
        protected static Element Insert<T>(Element src, int offset, int length) where T : Element
        {
            // Don't call this on anything other than untyped elements:
            if (!(src is ElementUntyped))
            {
                return null;
            }

            Document document = src.Document;
            Element newElement = (T)Activator.CreateInstance(typeof(T), document);
            newElement.Name = GetAutoName(document);
            int bytesPerItem = newElement.BytesPerItem;

            int actualLength = (length / bytesPerItem) * bytesPerItem; // Round down to the maximum number of complete items

            // Do we have enough bytes within the src element?
            if (actualLength == 0 || src.Length < offset + actualLength)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length,
                    $"There are not enough bytes in the buffer to insert type {newElement.TypeName}, a multiple of {bytesPerItem} bytes are required.");
            }

            int bytesBefore = offset;
            int bytesMiddle = actualLength;
            int bytesAfter  = src.Length - offset - actualLength;

            int offsetMiddle = offset;
            int offsetAfter  = offset + actualLength;

            newElement.Initialise(src.Offset + offsetMiddle, bytesMiddle);

            if (offsetMiddle > 0 && bytesAfter > 0)
            {
                ElementUntyped after = new ElementUntyped(document) {Name = GetAutoName(document)};
                after.Initialise(src.Offset + offsetAfter, bytesAfter);
                newElement.Append(after);
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

        public int Merge(IEnumerable<Element> selected)
        {
            int count = 0;
            Element prev = null;
            foreach (Element cur in selected)
            {
                if (prev != null && cur.PreviousElement == prev && cur.GetType() == prev.GetType())
                {
                    //Merge cur into prev
                    prev.Initialise(prev.Offset, prev.Length + cur.Length);
                    cur.Remove();
                    count++;
                }
                else
                {
                    prev = cur;
                }
            }

            if (count != 0)
            {
                RaiseSequenceChanged();
            }
            return count;
        }

        public int Convert<T>(IEnumerable<Element> selected) where T : Element
        {
            int count = 0;
            foreach (Element element in selected)
            {
                if (element.GetType() == typeof(T))
                {
                    continue;
                }
                Element newElement = (T)Activator.CreateInstance(typeof(T), this);

                //TODO: Check if the type of newElement can be converted without leaving any bytes

                newElement.Name = element.Name + "*";
                newElement.Initialise(element.Offset, element.Length);

                if (element == FirstElement)
                {
                    FirstElement = newElement;
                }
                element.Append(newElement);
                element.Remove();
                count++;
            }

            if (count != 0)
            {
                RaiseSequenceChanged();
            }
            return count;
        }
    }
}
