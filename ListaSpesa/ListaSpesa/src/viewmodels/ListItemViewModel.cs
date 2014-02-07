using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using ListaSpesa.Model;
using System.Linq;

namespace ListaSpesa.Viewmodels
{
    public abstract class ListItemViewModel : ViewModelBase
    {
        public abstract ObservableCollection<ListItem> ListOfItems { get; }

        /// <summary>
        /// Svuota la lista(se non esiste, la crea)
        /// </summary>
        public void ClearList()
        {
            ListOfItems.Clear();
        }

        public void CheckAll()
        {
            foreach (ListItem li in ListOfItems)
            {
                li.IsChecked = true;
            }
        }
    }
}
