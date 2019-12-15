using Hexalyser.Models.Elements;

namespace Hexalyser.ViewModels.Elements
{
    public class ElementUInt8ViewModel : ElementViewModel
    {
        private ElementUInt8 ElementUInt8 { get; set; }
        public ElementUInt8ViewModel(DocumentViewModel documentVm, Element element) : base(documentVm, element)
        {
            ElementUInt8 = (ElementUInt8)element;
        }

        public string ValuesPropertyName = "Values";
        public string Values
        {
            get
            {
                string s = "";
                int count = ElementUInt8.Values.Length;
                for (int i = 0; i < count; i++)
                {
                    s += $"{ElementUInt8.Values[i]}";
                    if (i < count - 1)
                    {
                        s += ", ";
                    }

                    if (i > 0 && i % 8 == 0)
                    {
                        s += "\n";
                    }
                }
                return s;
            }
        }
    }
}