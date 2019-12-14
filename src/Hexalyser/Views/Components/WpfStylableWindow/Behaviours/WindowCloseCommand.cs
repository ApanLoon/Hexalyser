using System;
using System.Windows;
using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace WpfStylableWindow.StylableWindow
// ReSharper restore CheckNamespace
{
    public class WindowCloseCommand :ICommand
    {     

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }
    }
}
