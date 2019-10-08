using System.Windows;

// ReSharper disable CheckNamespace
namespace WpfTextBoxBindableSelection
// ReSharper restore CheckNamespace
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        public static readonly DependencyProperty SelectionStartBindableProperty =
            DependencyProperty.Register (
                "SelectionStartBindable",
                typeof(int),
                typeof(TextBox),
                new PropertyMetadata(OnSelectionStartBindableChanged));

        public static readonly DependencyProperty SelectionLengthBindableProperty =
            DependencyProperty.Register (
                "SelectionLengthBindable",
                typeof(int),
                typeof(TextBox),
                new PropertyMetadata(OnSelectionLengthBindableChanged));

        private bool _changeFromUi;

        public TextBox()
        {
            SelectionChanged += OnSelectionChanged;
        }

        public int SelectionStartBindable
        {
            get => (int)GetValue(SelectionStartBindableProperty);
            set => SetValue(SelectionStartBindableProperty, value);
        }

        public int SelectionLengthBindable
        {
            get => (int)GetValue(SelectionLengthBindableProperty);
            set => SetValue(SelectionLengthBindableProperty, value);
        }

        private static void OnSelectionStartBindableChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (!(dependencyObject is TextBox textBox))
            {
                return;
            }

            if (!textBox._changeFromUi)
            {
                int newValue = (int)args.NewValue;
                textBox.SelectionStart = newValue;
            }
            else
            {
                textBox._changeFromUi = false;
            }
        }

        private static void OnSelectionLengthBindableChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (!(dependencyObject is TextBox textBox))
            {
                return;
            }

            if (!textBox._changeFromUi)
            {
                int newValue = (int)args.NewValue;
                textBox.SelectionLength = newValue;
            }
            else
            {
                textBox._changeFromUi = false;
            }
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (SelectionStartBindable != SelectionStart)
            {
                _changeFromUi = true;
                SelectionStartBindable = SelectionStart;
            }

            if (SelectionLengthBindable != SelectionLength)
            {
                _changeFromUi = true;
                SelectionLengthBindable = SelectionLength;
            }
        }
    }
}
