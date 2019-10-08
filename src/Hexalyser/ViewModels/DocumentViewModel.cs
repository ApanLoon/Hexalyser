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

        public ObservableCollection<ElementViewModel> Elements
        {
            get => _elements;
            set => Set(ref _elements, value);
        }
        private ObservableCollection<ElementViewModel> _elements;

        public string Length => $"{Document.Length} bytes";

        public ElementViewModel SelectedElement
        {
            get => _selectedElement;
            set => Set(ref _selectedElement, value);
        }
        private ElementViewModel _selectedElement;

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
            BuildElements();

            document.SequenceChanged += (d) => { BuildElements(); };
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
                        collection.Add(new ElementUntypedViewModel(e));
                        break;
                    case ElementUInt16 elementUInt16:
                        collection.Add(new ElementUInt16ViewModel(e));
                        break;
                    case ElementUInt32 elementUInt32:
                        collection.Add(new ElementUInt32ViewModel(e));
                        break;
                }
            }
            Elements = collection;
        }

    }
}
