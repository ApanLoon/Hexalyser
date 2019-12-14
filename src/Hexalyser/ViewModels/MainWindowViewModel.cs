
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hexalyser.Models;
using Hexalyser.Messages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Hexalyser.Models.Elements;
using Hexalyser.ViewModels.Commands;
using Hexalyser.ViewModels.Elements;

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

        #region ToolbarCommandProperties
        public const string BasicTypeCommandsPropertyName = "BasicTypeCommands";
        private ObservableCollection<Command> _basicTypeCommands = new ObservableCollection<Command>();
        public ObservableCollection<Command> BasicTypeCommands
        {
            get => _basicTypeCommands;
            set => Set(ref _basicTypeCommands, value);
        }
        #endregion ToolbarCommandProperties

        #endregion Properties

        #region Commands
        #region Command_Test
        public RelayCommand CommandTest
        {
            get { return _commandTest ?? (_commandTest = new RelayCommand(() =>
            {
                Document d = Documents[SelectedDocumentIndex].Document;
                Element e;
                switch (_testStep)
                {
                    case 4:
                        Document.InsertType["uint16"](d.FirstElement, 4);
                        //Document.InsertType["uint32"](d.FirstElement, 1); // Should throw an exception
                        break;
                    case 3:
                        _elementTest = Document.InsertType["uint16"](d.FirstElement.NextElement.NextElement, 0);
                        break;
                    case 2:
                        _elementTest = Document.InsertType["uint32"](_elementTest.NextElement, 0);
                        break;
                    case 1:
                        _elementTest = Document.InsertType["uint32"](_elementTest.NextElement, 0);
                        break;
                }

                if (_testStep > 0)
                {
                    _testStep--;
                }

                StatusMessage = $"{DateTime.Now.ToString()}: {Documents[SelectedDocumentIndex].Name} Remaining tests: {_testStep}";
            })); }
        }
        private RelayCommand _commandTest;
        private Element _elementTest;
        private int _testStep = 4;
        #endregion Command_Test

        #region Command_UInt16
        public RelayCommand CommandUInt16
        {
            get
            {
                return _commandUInt16 ??= new RelayCommand(() =>
                {
                    if (!BasicTypeCanExecute())
                    {
                        return;
                    }
                    DocumentViewModel dVm = Documents[SelectedDocumentIndex];
                    Document d = dVm.Document;
                    ElementViewModel eVm = dVm.SelectedElement;
                    //Document.InsertType["uint16"](d.FirstElement, 4);
                    StatusMessage = $"{DateTime.Now.ToString()}: Change to UInt16 at {eVm.SelectionStart}({eVm.SelectionLength}) in element starting at {eVm.Element.Offset} in {d.Name}";
                }, () => (true || BasicTypeCanExecute()));
            }
        }

        private RelayCommand _commandUInt16;
        #endregion Command_UInt16

        #region Command_UInt32
        public RelayCommand CommandUInt32
        {
            get
            {
                return _commandUInt32 ??= new RelayCommand(() =>
                {
                    if (!BasicTypeCanExecute())
                    {
                        return;
                    }
                    DocumentViewModel dVm = Documents[SelectedDocumentIndex];
                    Document d = dVm.Document;
                    ElementViewModel eVm = dVm.SelectedElement;
                    //Document.InsertType["uint32"](d.FirstElement, 4);
                    StatusMessage = $"{DateTime.Now.ToString()}: Change to UInt32 at {eVm.SelectionStart}({eVm.SelectionLength}) in element starting at {eVm.Element.Offset} in {d.Name}";
                }, () => (true || BasicTypeCanExecute()));
            }
        }
        private RelayCommand _commandUInt32;
        #endregion Command_UInt32

        private bool BasicTypeCanExecute()
        {
            // TODO: What is the best way to make this re-check when selection changes?
            if (SelectedDocumentIndex == -1)
            {
                return false;
            }

            DocumentViewModel dVm = Documents[SelectedDocumentIndex];
            Document d = dVm.Document;
            if (d == null)
            {
                return false;
            }

            ElementViewModel eVm = dVm.SelectedElement;
            if (eVm == null)
            {
                return false;
            }

            return true;
        }

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

                #region BasicTypeCommands
                BasicTypeCommands.Add(new Command() { Name = "UInt16", Execute = CommandUInt16 });
                BasicTypeCommands.Add(new Command() { Name = "UInt32", Execute = CommandUInt32 });
                #endregion BasicTypeCommands


                Documents = new ObservableCollection<DocumentViewModel>(Model.Documents.Select(document => new DocumentViewModel(document)));

                Messenger.Default.Register<FilesOpenedMessage>(this, OpenDocuments);
                Messenger.Default.Register<CloseDocumentMessage>(this, CloseDocument);
            });
        }
        #endregion Constructors
    }
}
