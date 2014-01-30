using GalaSoft.MvvmLight;

namespace ListaSpesa.Viewmodels
{
    public abstract class ListItemViewModel : ViewModelBase
    {
        public abstract bool IsAddToFavouritesVisible { get; }
    }
}
