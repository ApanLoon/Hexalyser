using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hexalyser.Models;
using Hexalyser.Messages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

        public const string EditCommandsPropertyName = "EditCommands";
        private ObservableCollection<Command> _editCommands = new ObservableCollection<Command>();
        public ObservableCollection<Command> EditCommands
        {
            get => _editCommands;
            set => Set(ref _editCommands, value);
        }
        #endregion ToolbarCommandProperties

        #endregion Properties

        #region Commands
        #region Command_Test
        public RelayCommand CommandTest
        {
            get 
            {
                return _commandTest ?? (_commandTest = new RelayCommand(() =>
                {
                    StatusMessage = $"{DateTime.Now.ToString()}: Test";
                }));
            }
        }
        private RelayCommand _commandTest;
        #endregion Command_Test
        #region Command_UInt8
        public RelayCommand CommandUInt8
        {
            get
            {
                return _commandUInt8 ??= new RelayCommand(() => { InsertType("uint8"); }, BasicTypeCanExecute);
            }
        }
        private RelayCommand _commandUInt8;
        #endregion Command_UInt8
        #region Command_UInt16
        public RelayCommand CommandUInt16
        {
            get
            {
                return _commandUInt16 ??= new RelayCommand(() => { InsertType("uint16"); }, BasicTypeCanExecute);
            }
        }
        private RelayCommand _commandUInt16;
        #endregion Command_UInt16
        #region Command_UInt32
        public RelayCommand CommandUInt32
        {
            get
            {
                return _commandUInt32 ??= new RelayCommand(() => { InsertType("uint32"); }, BasicTypeCanExecute);
            }
        }
        private RelayCommand _commandUInt32;
        #endregion Command_UInt32

        #region Command_Untyped
        public RelayCommand CommandUntyped
        {
            get
            {
                return _commandUntyped ??= new RelayCommand(() =>
                {
                    DocumentViewModel dVm = Documents[SelectedDocumentIndex];

                    if (dVm.SelectedElements.Count == 0)
                    {
                        return;
                    }

                    int convertCount = dVm.Document.Convert<ElementUntyped>(dVm.SelectedElements.OrderBy(eVm => eVm.Element.Offset).Select(eVm => eVm.Element));

                    if (convertCount > 0)
                    {
                        StatusMessage = $"{DateTime.Now.ToString()}: Converted {convertCount} elements.";
                    }
                    else
                    {
                        StatusMessage = $"{DateTime.Now.ToString()}: No elements could be converted.";
                    }
                });
            }
        }
        private RelayCommand _commandUntyped;
        #endregion Command_Untyped
        #region Command_Merge
        public RelayCommand CommandMerge
        {
            get
            {
                return _commandMerge ??= new RelayCommand(() =>
                {
                    DocumentViewModel dVm = Documents[SelectedDocumentIndex];

                    if (dVm.SelectedElements.Count < 2)
                    {
                        return;
                    }

                    int mergeCount = dVm.Document.Merge(dVm.SelectedElements.OrderBy(eVm => eVm.Element.Offset).Select(eVm => eVm.Element));
                    if (mergeCount > 0)
                    {
                        StatusMessage = $"{DateTime.Now.ToString()}: Merged {mergeCount} elements.";
                    }
                    else
                    {
                        StatusMessage = $"{DateTime.Now.ToString()}: No elements could be merged.";
                    }
                });
            }
        }
        private RelayCommand _commandMerge;
        #endregion Command_Untyped

        #region CommandTools
        private void InsertType(string typeName)
        {
            if (!BasicTypeCanExecute())
            {
                return;
            }

            try
            {
                DocumentViewModel dVm = Documents[SelectedDocumentIndex];
                Document d = dVm.Document;
                ElementViewModel eVm = dVm.SelectedElement;
                Element e = eVm.Element;
                Document.InsertType[typeName](e, eVm.SelectionStart, eVm.SelectionLength);
                StatusMessage = $"{DateTime.Now.ToString()}: Changed to {typeName} at {eVm.SelectionStart}({eVm.SelectionLength}) in element starting at {eVm.Element.Offset} in {d.Name}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"{DateTime.Now.ToString()}: Error. {ex.Message}";
            }
        }
        
        private bool BasicTypeCanExecute()
        {
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
        #endregion CommandTools
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
        #region SelectionChanged
        protected void SelectionChanged(SelectionChangedMessage m)
        {
            foreach (Command command in BasicTypeCommands)
            {
                command.Execute.RaiseCanExecuteChanged();
            }
        }
        #endregion SelectionChanged
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
                BasicTypeCommands.Add(new Command() { Name = "UInt8",  Execute = CommandUInt8 });
                BasicTypeCommands.Add(new Command() { Name = "UInt16", Execute = CommandUInt16 });
                BasicTypeCommands.Add(new Command() { Name = "UInt32", Execute = CommandUInt32 });
                #endregion BasicTypeCommands

                #region EditCommands
                EditCommands.Add(new Command() { Name = "Untyped", Execute = CommandUntyped });
                EditCommands.Add(new Command() { Name = "Merge",   Execute = CommandMerge });
                #endregion EditCommands


                Documents = new ObservableCollection<DocumentViewModel>(Model.Documents.Select(document => new DocumentViewModel(document)));

                Messenger.Default.Register<FilesOpenedMessage>(this, OpenDocuments);
                Messenger.Default.Register<CloseDocumentMessage>(this, CloseDocument);
                Messenger.Default.Register<SelectionChangedMessage>(this, SelectionChanged);
            });
        }
        #endregion Constructors
    }
}
