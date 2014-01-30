using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ListaSpesa.Commands;
using ListaSpesa.Model;
using ListaSpesa.Utils;
using Microsoft.Phone.Controls;

namespace ListaSpesa.Viewmodels
{
    public delegate void RemoveItemEventHandler(MenuItem item);
    public class MainPageViewModel : ListItemViewModel
    {
        
        #region Costruttore
        /// <summary>
        /// Costruttore
        /// </summary>
        public MainPageViewModel()
        {
            _listOfItems = new ObservableCollection<ListItem>();
            _listOfItems.CollectionChanged += _listOfItems_CollectionChanged;

            AddItemCommand = new RelayCommand(AddItem, CanAddItem);
            NewItemText = string.Empty;
            ShowInput = true;
        }
        #endregion

        #region Properties

        #region ListOfItems
        private const string _listOfItemsPrpName = "ListOfItems";
        private readonly ObservableCollection<ListItem> _listOfItems;

        public ObservableCollection<ListItem> ListOfItems
        {
            get { return _listOfItems; }
        }

        public override bool IsAddToFavouritesVisible
        {
            get { return true; }
        }


        #endregion

        #region IsSpesaFinished

        private const string _isSpesaFinishedPrpName = "IsSpesaFinished";
        public bool IsSpesaFinished
        {
            get
            {
                return (!IsSpesaEmpty) && (GetNumberOfMissingItems() == 0);
            }
        }

        private void RaiseIsSpesaFinished()
        {
            RaisePropertyChanged(_isSpesaFinishedPrpName);
        }
        #endregion

        #region Summary
        private const string _summaryPrpName = "Summary";
        public string Summary
        {
            get { return GetSummary(); }
        }

        private void RaiseSummaryChanged()
        {
            RaisePropertyChanged(_summaryPrpName);
        }
        #endregion

        #region NewItemText
        private const string _newItemTextPrpName = "NewItemText";
        private string _newItemText;

        public string NewItemText
        {
            get { return _newItemText; }
            set
            {
                _newItemText = value;
                RaisePropertyChanged(_newItemTextPrpName);
                //Notifico che deve essere rivalutata la abilitazione del comando
                AddItemCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region ShowInput
        private const string _showInputPrpName = "ShowInput";
        private bool _showInput;
        public bool ShowInput
        {
            get { return _showInput; }
            set
            {
                _showInput = value;
                RaisePropertyChanged(_showInputPrpName);
            }
        }
        #endregion

        #region IsSpesaEmpty

        private bool IsSpesaEmpty
        {
            get { return _listOfItems.Count == 0; }
        }

        #endregion

        #endregion

        #region Commands
        public RelayCommand AddItemCommand { get; private set; }

        /// <summary>
        /// Dice se è abilitato il button di aggiunta elemento alla spesa
        /// </summary>
        private bool CanAddItem()
        {
            return !string.IsNullOrEmpty(_newItemText); 
        }

        /// <summary>
        /// Aggiunge un elemento alla spesa
        /// </summary>
        private void AddItem()
        {
            ListItem li = new ListItem(NewItemText);
            li.RemoveItemRequested += li_RemoveItemRequested;
            AddItemToList(li);
            ResetTextbox();
        }

        void li_RemoveItemRequested(MenuItem item)
        {
            RemoveItem(item.DataContext as ListItem);
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
            _listOfItems.Clear();
        }

        public void RemoveItem(ListItem li)
        {
            _listOfItems.Remove(li);
        }
        #endregion

        #region Metodi privati

        private void ResetTextbox()
        {
            NewItemText = string.Empty;
        }

        private void AddItemToList(ListItem item)
        {
            item.PropertyChanged += item_PropertyChanged;
            _listOfItems.Add(item);
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


    }
}
