using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ListaSpesa.Commands;
using ListaSpesa.Model;

namespace ListaSpesa.Viewmodel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        //Membri privati
        private ObservableCollection<ListItem> _listOfItems;
        private string _newItemText;
        private bool _showInput;

        public ObservableCollection<ListItem> Items
        {
            get { return _listOfItems; }
        }
        public string Summary
        {
            get { return GetSummary(); }
        }
        public string NewItemText
        {
            get { return _newItemText; }
            set
            {
                _newItemText = value;
                OnPropertyChanged("NewItemText");
                //Notifico che deve essere rivalutata la abilitazione del comando
                AddItemCommand.RaiseCanExecuteChanged();
            }
        }
        public bool ShowInput
        {
            get { return _showInput; }
            set
            {
                _showInput = value;
                OnPropertyChanged("ShowInput");
            }
        }
        public bool IsSpesaFinished
        {
            get
            {
                return (!IsSpesaEmpty) && (GetNumberOfMissingItems()==0);
            }
        }

        //Commands
        public DelegateCommand AddItemCommand { get; private set; }

        #region Costruttore
        /// <summary>
        /// Costruttore
        /// </summary>
        public MainPageViewModel()
        {
            _listOfItems = new ObservableCollection<ListItem>();

            _listOfItems.CollectionChanged += _listOfItems_CollectionChanged;
            AddItemCommand = new DelegateCommand((o) => AddItem(), (o) => CanAddItem);
            NewItemText = string.Empty;
            ShowInput = true;
        }
        #endregion

        #region Metodi public
        public void SetItems(ObservableCollection<ListItem> items)
        {
            ClearList();
            foreach (var listItem in items)
            {
                AddItemToList(listItem);
            }
        }

        /// <summary>
        /// Svuota la lista(se non esiste, la crea)
        /// </summary>
        public void ClearList()
        {
            if (_listOfItems == null)
            {
                _listOfItems = new ObservableCollection<ListItem>();
            }
            else
            {
                _listOfItems.Clear();
            }
        }

        public void RemoveItem(ListItem li)
        {
            _listOfItems.Remove(li);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Dice se è abilitato il button di aggiunta elemento alla spesa
        /// </summary>
        private bool CanAddItem
        {
            //get { return true; }
            get { return !string.IsNullOrEmpty(_newItemText); }
        }

        /// <summary>
        /// Aggiunge un elemento alla spesa
        /// </summary>
        private void AddItem()
        {
            ////get the text
            //if (string.IsNullOrEmpty(NewItemText))
            //{
            //    MessageBox.Show(Messages.MsgValidItem);
            //    return;
            //}
            AddItemToList(new ListItem(NewItemText));
            ResetTextbox();
        }
        #endregion

        #region Metodi privati
        private void RaiseSummaryChanged()
        {
            OnPropertyChanged("Summary");

        }

        private void RaiseIsSpesaFinished()
        {
            OnPropertyChanged("IsSpesaFinished");
        }

        private void ResetTextbox()
        {
            NewItemText = string.Empty;
        }

        private void AddItemToList(ListItem item)
        {
            if (_listOfItems == null)
            {
                ClearList();
            }
            if (_listOfItems != null)
            {
                item.PropertyChanged += item_PropertyChanged;
                _listOfItems.Add(item);
            }
        }

        private bool IsSpesaEmpty
        {
            get { return ((_listOfItems == null) || (_listOfItems.Count == 0)); }
        }

        private string GetSummary()
        {
            if (IsSpesaEmpty) return Messages.MsgSpesaVuota;
            int numberOfMissingItems = GetNumberOfMissingItems();
            switch (numberOfMissingItems)
            {
                case 0:
                    return Messages.MsgSpesaCompletata;
                case 1:
                    return Messages.MsgManca1;
                default:
                    return string.Format(Messages.MsgMancanoN, numberOfMissingItems);
            }
        }

        private int GetNumberOfMissingItems()
        {
            return _listOfItems.Count(p => !p.IsChecked);
        }
        #endregion

        #region Event handler
        private void _listOfItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaiseSummaryChanged();
            RaiseIsSpesaFinished();
        }


        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseSummaryChanged();
            RaiseIsSpesaFinished();
        }
        #endregion

        #region INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prpName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prpName));
            }
        }
        #endregion
    }
}
