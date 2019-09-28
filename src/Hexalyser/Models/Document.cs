using System;
using System.Collections.Generic;
using System.IO;

namespace Hexalyser.Models
{
    public class Document
    {
        #region Properties
        public string Name { get; set; }
        public string FileName { get; set; }
        public int Length { get; protected set; }

        public List<Element> Elements = new List<Element>();
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
                Elements.Add(new Element(buf));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion Constructors
    }
}
