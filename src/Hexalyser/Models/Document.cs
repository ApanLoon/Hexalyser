using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hexalyser.Models.Elements;

namespace Hexalyser.Models
{
    public class Document
    {
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

        protected Element FirstElement;

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

                byte[] buf = File.ReadAllBytes(fileName);
                Length = buf.Length;
                FirstElement = new ElementUntyped(buf, this);

                //TODO: Remove test code: (This might be somewhat correct for PDB files)
                Element e;
                e = InsertType["uint16"](FirstElement, 4);
                //InsertType["uint32"](FirstElement, 1); // Should throw an exception
                e = InsertType["uint16"](FirstElement.NextElement.NextElement, 0);
                e = InsertType["uint32"](e.NextElement, 0);
                e = InsertType["uint32"](e.NextElement, 0);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
        #endregion Constructors

        public static Dictionary<string, Func<Element, int, Element>> InsertType = new Dictionary<string, Func<Element, int, Element>>()
        {
            {"uint16", (src, offset) => Insert<ElementUInt16>(src, offset, 2)},
            {"uint32", (src, offset) => Insert<ElementUInt32>(src, offset, 4)}
        };

        protected static Element Insert<T>(Element src, int offset, int length) where T : Element
        {
            if (!(src is ElementUntyped)) // Don't call this on anything other than untyped elements
            {
                return null;
            }

            Document document = src.Document;
            if (src.Bytes.Length < offset + length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), src.Bytes.Length - offset,
                    $"There are not enough bytes in the buffer to insert type {typeof(T).Name}, {length} bytes are required.");
            }
            byte[] newBuf = src.Bytes.Skip(offset).Take(length).ToArray();
            byte[] nextBytes = src.Bytes.Skip(offset + length).ToArray();

            Element newElement = (T)Activator.CreateInstance(typeof(T), newBuf, document); 

            if (src.Bytes.Length - offset - length > 0)
            {
                newElement.Append(new ElementUntyped(nextBytes, document));
            }

            if (offset > 0)
            {
                byte[] previousBytes = src.Bytes.Take(offset).ToArray();
                src.Bytes = previousBytes;
                src.Append(newElement);
            }
            else
            {
                // We have no bytes in front of us, we need to remove src and link in the new element to the previous one
                if (src.PreviousElement != null)
                {
                    src.PreviousElement.Append(newElement);
                }
                else
                {
                    // This was the first element
                    if (src.NextElement != null)
                    {
                        src.Prepend(newElement);
                    }
                    document.FirstElement = newElement;
                }
            }
            document.RaiseSequenceChanged();
            return newElement;
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
