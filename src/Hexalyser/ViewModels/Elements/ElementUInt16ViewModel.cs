using Hexalyser.Models.Elements;

namespace Hexalyser.ViewModels.Elements
{
    public class ElementUInt16ViewModel : ElementViewModel
    {
        private ElementUInt16 ElementUInt16 { get; set; }
        public ElementUInt16ViewModel(Element element) : base(element)
        {
            ElementUInt16 = (ElementUInt16)element;
        }

        public string ValuesPropertyName = "Values";
        public string Values
        {
            get
            {
                string s = "";
                int count = ElementUInt16.Values.Length;
                for (int i = 0; i < count; i++)
                {
                    s += $"{ElementUInt16.Values[i]}";
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
