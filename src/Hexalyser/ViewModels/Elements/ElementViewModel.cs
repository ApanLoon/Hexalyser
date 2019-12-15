using GalaSoft.MvvmLight;
using Hexalyser.Models.Elements;

namespace Hexalyser.ViewModels.Elements
{
    public class ElementViewModel : ViewModelBase
    {
        protected ElementViewModel(DocumentViewModel documentVm, Element element)
        {
            DocumentVm = documentVm;
            Element = element;
        }

        public DocumentViewModel DocumentVm { get; protected set; }
        public Element Element { get; protected set; }

        public string TypeName => Element.TypeName;

        public string NamePropertyName = "Name";
        public string Name
        {
            get => Element.Name;
            set
            {
                Element.Name = value;
                RaisePropertyChanged();
            } 
        }

        public string CountPropertyName = "Count";
        public string Count
        {
            get => Element.Count.Text;
            set
            {
                Element.Count.Text = value;
                RaisePropertyChanged();
            }
        }

        public int SelectionStart
        {
            get => _selectionStart;
            set => Set(ref _selectionStart, value);
        }
        private int _selectionStart;

        public int SelectionLength
        {
            get => _selectionLength;
            set => Set(ref _selectionLength, value);
        }
        private int _selectionLength;


    }
}
