using System.Windows;
using System.Windows.Controls;

namespace Hexalyser.Views
{
    /// <summary>
    /// Interaction logic for ElementView.xaml
    /// </summary>
    public partial class ElementView : UserControl
    {

        public object ValueContent
        {
            get => (object)GetValue(ValueContentProperty);
            set => SetValue(ValueContentProperty, value);
        }

        public static readonly DependencyProperty ValueContentProperty = DependencyProperty.Register
        (
            "ValueContent",
            typeof(object),
            typeof(ElementView),
            new PropertyMetadata(null)
        );

        public Visibility CountVisibility
        {
            get => (Visibility)GetValue(CountVisibilityProperty);
            set => SetValue(CountVisibilityProperty, value);
        }

        public static readonly DependencyProperty CountVisibilityProperty = DependencyProperty.Register
        (
            "CountVisibility",
            typeof(Visibility),
            typeof(ElementView),
            new PropertyMetadata(null)
        );

        public ElementView()
        {
            InitializeComponent();
        }
    }
}
