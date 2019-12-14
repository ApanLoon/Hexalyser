using System;
using System.Windows;
using System.Windows.Input;

namespace WpfStylableWindow.StylableWindow
{
    public class WindowMaximizeCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (!(parameter is Window window)) return;

            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
    }
}
