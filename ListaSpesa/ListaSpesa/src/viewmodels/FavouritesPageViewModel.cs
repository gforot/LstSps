using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ListaSpesa.Model;
using System.Linq;

namespace ListaSpesa.Viewmodels
{
    public class FavouritesPageViewModel : ListItemViewModel
    {
        #region Properties

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
    }
}
