
using GalaSoft.MvvmLight.Messaging;
using Hexalyser.ViewModels;
using Hexalyser.ViewModels.Elements;

namespace Hexalyser.Messages
{
    public class SelectionChangedMessage : GenericMessage<SelectionMessageParameters>
    {
        public SelectionChangedMessage(SelectionMessageParameters content) : base(content)
        {
        }
    }

    public class SelectionMessageParameters
    {
        public DocumentViewModel DocumentVm { get; set; }
        public ElementViewModel ElementVm { get; set; }
        public int SelectionStart { get; set; }
        public int SelectionLength { get; set; }
    }
}
