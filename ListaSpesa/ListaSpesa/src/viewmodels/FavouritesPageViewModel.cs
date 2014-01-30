using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ListaSpesa.Model;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;

namespace ListaSpesa.Viewmodels
{
    public class FavouritesPageViewModel : ListItemViewModel
    {
        public RelayCommand AddItemCommand { get; private set; }

        #region Properties
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
        #region ListOfItems
        private const string _listOfItemsPrpName = "ListOfItems";
        private readonly ObservableCollection<ListItem> _listOfItems;

        public ObservableCollection<ListItem> ListOfItems
        {
            get { return _listOfItems; }
        }
        #endregion
        #endregion

        public FavouritesPageViewModel()
        {
            _listOfItems = new ObservableCollection<ListItem>();
            
            AddItemCommand = new RelayCommand(AddItem, CanAddItem);
            NewItemText = string.Empty;
        }

        public void SetItems(ObservableCollection<ListItem> items)
        {
            ClearList();
            foreach (var listItem in items)
            {
                AddItemToList(listItem);
            }
        }

        private void ClearList()
        {
            _listOfItems.Clear();
        }

        private void AddItemToList(ListItem item)
        {
            item.PropertyChanged += item_PropertyChanged;
            _listOfItems.Add(item);
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //RaiseSummaryChanged();
            //RaiseIsSpesaFinished();
        }

        public override bool IsAddToFavouritesVisible
        {
            get { return false; }
        }

        public void UncheckAll()
        {
            foreach (ListItem item in _listOfItems)
            {
                item.IsChecked = false;
            }
        }

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
            AddItem(NewItemText);
            ResetTextbox();
        }

        private void ResetTextbox()
        {
            NewItemText = string.Empty;
        }

        public void AddItem(string text)
        {
            ListItem li = new ListItem(text);
            li.RemoveItemRequested += li_RemoveItemRequested;
            AddItemToList(li);
        }

        void li_RemoveItemRequested(MenuItem item)
        {
            RemoveItem(item.DataContext as ListItem);
        }

        public void RemoveItem(ListItem li)
        {
            _listOfItems.Remove(li);
        }
    }
}
