using System.Collections.Generic;
using System.IO;

namespace Hexalyser.Models
{
    public class Model
    {
        public List<Document> Documents = new List<Document>();

        public Document OpenDocument(string fileName)
        {
            Document document = new Document(fileName);
            Documents.Add(document);
            return document;
        }

        public void CloseDocument(Document document)
        {
            if (Documents.Contains(document))
            {
                Documents.Remove(document);
            }
        }
    }
}
