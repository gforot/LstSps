using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using ListaSpesa.Model;
using System.Linq;

namespace ListaSpesa.Viewmodels
{
    public abstract class ListItemViewModel : ViewModelBase
    {
        public abstract bool IsAddToFavouritesVisible { get; }

        public abstract ObservableCollection<ListItem> ListOfItems { get; }

        public List<int> GetUsedIds()
        {
            return new List<int>(ListOfItems.Select(li => li.Id));
        }
    }
}
