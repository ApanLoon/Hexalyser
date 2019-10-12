using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Hexalyser.Messages;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

namespace Hexalyser.Behaviours
{
    /* TODO: For some reason this class gives an error unless I run this in a developer command prompt with administrator privileges:
             cd C:\Program Files (x86)\Microsoft SDKs\Expression\Blend\.NETFramework\v4.5\Libraries\
             Register DLL: gacutil -i System.Windows.Interactivity.dll
     */
    public class OpenFileBehaviour : Behavior<MenuItem>
    {
        public string MessageIdentifier { get; set; }
        public string Filter { get; set; }
        public bool MultiSelect { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObject_Click;
        }

        void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            // Open the dialog and send the message
            var dialog = new OpenFileDialog();
            dialog.Filter = "All files (*.*)|*.*";
            dialog.Multiselect = MultiSelect;
            if (dialog.ShowDialog() == true)
            {
                Messenger.Default.Send(new FilesOpenedMessage(dialog.FileNames) { Identifier = MessageIdentifier });
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
            base.OnDetaching();
        }
    }
}
