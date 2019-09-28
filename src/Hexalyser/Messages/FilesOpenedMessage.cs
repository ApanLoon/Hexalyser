using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;

namespace Hexalyser.Messages
{
    public class FilesOpenedMessage : GenericMessage<IEnumerable<string>>
    {
        public FilesOpenedMessage(IEnumerable<string> fileNames) : base(fileNames)
        {
            Identifier = string.Empty;
        }

        public string Identifier { get; set; }
    }
}
