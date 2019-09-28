using GalaSoft.MvvmLight.Messaging;
using Hexalyser.ViewModels;

namespace Hexalyser.Messages
{
    public class CloseDocumentMessage : GenericMessage<DocumentViewModel>
    {
        public CloseDocumentMessage(DocumentViewModel vm) : base(vm)
        {
        }
    }
}
