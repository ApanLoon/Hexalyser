using Hexalyser.Models.Elements;

namespace Hexalyser.ViewModels.Elements
{
    public class ElementUInt32ViewModel : ElementViewModel
    {
        private ElementUInt32 ElementUInt32 { get; set; }
        public ElementUInt32ViewModel(Element element) : base(element)
        {
            ElementUInt32 = (ElementUInt32)element;
        }

        public string ValuesPropertyName = "Values";
        public string Values
        {
            get
            {
                string s = "";
                int count = ElementUInt32.Values.Length;
                for (int i = 0; i < count; i++)
                {
                    s += $"{ElementUInt32.Values[i]}";
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