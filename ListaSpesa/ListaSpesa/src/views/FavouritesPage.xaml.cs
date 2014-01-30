using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ListaSpesa.src.views
{
    public partial class FavouritesPage : PhoneApplicationPage
    {
        public FavouritesPage()
        {
            InitializeComponent();

            this.DataContext = App.Current.FavouritesViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.Current.FavouritesViewModel.UncheckAll();
        }
    }
}