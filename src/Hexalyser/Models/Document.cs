using System.IO;

namespace Hexalyser.Models
{
    public class Document
    {
        #region Properties
        public string Name { get; set; }
        public string FileName { get; set; }
        #endregion Properties

        #region Constructors
        public Document (string fileName)
        {
            FileName = fileName;
            Name = Path.GetFileName(fileName);

            // TODO: Open the actual file
        }
        #endregion Constructors
    }
}
