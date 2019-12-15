using System.Collections;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Hexalyser.Messages;
using Hexalyser.Models;
using Hexalyser.Models.Elements;
using Hexalyser.ViewModels.Elements;

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

        public string ElementsPropertyName = "Elements";
        private ObservableCollection<ElementViewModel> _elements;
        public ObservableCollection<ElementViewModel> Elements
        {
            get => _elements;
            set => Set(ref _elements, value);
        }

        public string Length => $"{Document.Length} bytes";

        private ElementViewModel _selectedElement;
        public ElementViewModel SelectedElement
        {
            get => _selectedElement;
            set => Set(ref _selectedElement, value);
        }

        public string SelectedElementsPropertyName = "SelectedElements";
        private ObservableCollection<ElementViewModel> _selectedElements = new ObservableCollection<ElementViewModel>();
        public ObservableCollection<ElementViewModel> SelectedElements
        {
            get => _selectedElements;
            set => Set(ref _selectedElements, value);
        }

        public IList SelectedItems
        {
            get => SelectedElements;
            set
            {
                SelectedElements.Clear();
                foreach (ElementViewModel eVm in value)
                {
                    SelectedElements.Add(eVm);
                }
            }
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

        #region MessageHandlers
        #region SelectionChanged
        protected void SelectionChanged(SelectionChangedMessage m)
        {
            if (m.Content.DocumentVm == this)
            {
                SelectedElement = m.Content.ElementVm;
            }
        }
        #endregion SelectionChanged
        #endregion MessageHandlers

        #region ProtectedProperties
        #endregion ProtectedProperties

        #region Constructors
        public DocumentViewModel(Document document)
        {
            Document = document;
            BuildElements();

            document.SequenceChanged += (d) => { BuildElements(); };
            Messenger.Default.Register<SelectionChangedMessage>(this, SelectionChanged);
        }
        #endregion Constructors

        private void BuildElements()
        {
            ObservableCollection<ElementViewModel> collection = new ObservableCollection<ElementViewModel>();
            foreach (Element e in Document.Elements)
            {
                switch (e)
                {
                    case ElementUntyped elementUntyped:
                        collection.Add(new ElementUntypedViewModel(this, e));
                        break;
                    case ElementUInt8 elementUInt8:
                        collection.Add(new ElementUInt8ViewModel(this, e));
                        break;
                    case ElementUInt16 elementUInt16:
                        collection.Add(new ElementUInt16ViewModel(this, e));
                        break;
                    case ElementUInt32 elementUInt32:
                        collection.Add(new ElementUInt32ViewModel(this, e));
                        break;
                }
            }
            Elements = collection;
        }

    }
}
