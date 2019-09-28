using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Hexalyser.Messages;
using Hexalyser.Models;

namespace Hexalyser.ViewModels
{
    public class DocumentViewModel : ViewModelBase
    {
        #region Properties
        public Document Document { get; protected set; }

        public string Name
        {
            get => Document.Name;
            set => Set(Document.Name, ref value);
        }
        public string FileName
        {
            get => Document.FileName;
            set => Set(Document.FileName, ref value);
        }
        #endregion Properties

        #region Commands
        #region Command_Close

        public RelayCommand CommandClose
        {
            get { return _commandClose ??= new RelayCommand(() => { Messenger.Default.Send(new CloseDocumentMessage(this)); }); }
        }

        private RelayCommand _commandClose;

        #endregion Command_Close
        #endregion Commands
        
        #region ProtectedProperties
        #endregion ProtectedProperties

        #region Constructors
        public DocumentViewModel(Document document)
        {
            Document = document;
        }
        #endregion Constructors
    }
}
