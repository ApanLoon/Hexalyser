using GalaSoft.MvvmLight;
using Hexalyser.Models.Elements;

namespace Hexalyser.ViewModels.Elements
{
    public class ElementViewModel : ViewModelBase
    {
        protected ElementViewModel(Element element)
        {
            Element = element;
        }

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
    }
}
