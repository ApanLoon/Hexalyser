
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hexalyser.Models;
using Hexalyser.Messages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight.Messaging;

namespace Hexalyser.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties
        public string WindowTitle
        {
            get
            {
                Assembly a = Assembly.GetExecutingAssembly();
                AssemblyName n = a.GetName();
                return $"{n.Name} v{n.Version.Major}.{n.Version.Minor}";
            }
        }

        public ObservableCollection<DocumentViewModel> Documents
        {
            get => _documents;
            set => Set(ref _documents, value);
        }
        private ObservableCollection<DocumentViewModel> _documents;

        public string StatusMessage
        {
            get => _statusMessage;
            set => Set(ref _statusMessage, value);
        }
        private string _statusMessage;

        public const string SelectedDocumentIndexPropertyName = "SelectedDocumentIndex";
        private int _selectedDocumentIndex = -1;
        public int SelectedDocumentIndex
        {
            get => _selectedDocumentIndex;
            set => Set(ref _selectedDocumentIndex, value);
        }

        #endregion Properties

        #region Commands
        #region Command_Test
        public RelayCommand CommandTest
        {
            get { return _commandTest ?? (_commandTest = new RelayCommand(() => { StatusMessage = DateTime.Now.ToString(); })); }
        }
        private RelayCommand _commandTest;
        #endregion Command_Test
        #endregion Commands

        #region MessageHandlers
        #region OpenDocuments
        protected void OpenDocuments(FilesOpenedMessage m)
        {
            if (m.Identifier != "OpenDocuments")
            {
                return;
            }

            foreach (string fileName in m.Content)
            {
                Document document = Model.OpenDocument(fileName);
                DocumentViewModel vm = new DocumentViewModel(document);
                Documents.Add(vm);
                SelectedDocumentIndex = Documents.Count - 1;
            }
        }
        #endregion Open
        #region CloseDocument
        protected void CloseDocument(CloseDocumentMessage m)
        {
            if (Documents.Contains(m.Content))
            {
                //TODO: Check if the file is modified and, if so, pop up a confirmation dialog.
                Documents.Remove(m.Content);
                Model.CloseDocument(m.Content.Document);
            }
        }
        #endregion CloseDocument
        #endregion MessageHandlers

        #region ProtectedProperties
        Model Model;
        #endregion ProtectedProperties

        #region Constructors
        public MainWindowViewModel(IModelService modelService)
        {
            modelService.GetModel((model, exception) =>
            {
                if (exception != null)
                {
                    throw exception;
                }

                Model = model;

                Documents = new ObservableCollection<DocumentViewModel>(Model.Documents.Select(document => new DocumentViewModel(document)));

                Messenger.Default.Register<FilesOpenedMessage>(this, OpenDocuments);
                Messenger.Default.Register<CloseDocumentMessage>(this, CloseDocument);
            });
        }
        #endregion Constructors
    }
}
