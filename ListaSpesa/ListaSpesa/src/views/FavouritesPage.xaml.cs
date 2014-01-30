using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using ListaSpesa.Model;
using ListaSpesa.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ListaSpesa.src.views
{
    public partial class FavouritesPage : PhoneApplicationPage
    {
        public FavouritesPage()
        {
            InitializeComponent();

            BuildLocalizedApplicationBar();
            this.DataContext = App.Current.FavouritesViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.Current.FavouritesViewModel.UncheckAll();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = sender as TextBox;
                // Update the binding source
                BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);
                if (bindingExpr != null)
                {
                    bindingExpr.UpdateSource();
                }
            }
        }

        #region ApplicationBar

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            #region Add All
            ApplicationBarIconButton appBarButtonAddAll =
                new ApplicationBarIconButton(new
                Uri("/Images/appbar.add.rest.png", UriKind.Relative));
            appBarButtonAddAll.Click += appBarButtonAddAll_Click;
            appBarButtonAddAll.Text = AppResources.AddAll;
            #endregion

            #region Add Selected
            ApplicationBarIconButton appBarButtonAddSelected =
                new ApplicationBarIconButton(new
                Uri("/Images/appbar.add.rest.png", UriKind.Relative));
            appBarButtonAddSelected.Click += appBarButtonAddSelected_Click;
            appBarButtonAddSelected.Text = AppResources.AddSelected;
            #endregion

            //#region Empty
            //// Create a new button and set the text value to the localized string from AppResources.
            //ApplicationBarIconButton appBarButtonEmpty =
            //    new ApplicationBarIconButton(new
            //    Uri("/Images/appbar.cancel.rest.png", UriKind.Relative));
            //appBarButtonEmpty.Click += appBarButtonEmpty_Click;
            //appBarButtonEmpty.Text = AppResources.Svuota;
            //#endregion

            //#region Favourites
            //ApplicationBarIconButton appBarButtonFavs =
            //     new ApplicationBarIconButton(new Uri("/Images/appbar.favs.rest.png", UriKind.Relative));
            //appBarButtonFavs.Click += appBarButtonFavs_Click;
            //appBarButtonFavs.Text = AppResources.Favourites;
            //#endregion

            //#region Info
            //ApplicationBarIconButton appBarButtonInfo =
            //    new ApplicationBarIconButton(new Uri("/Images/appbar.questionmark.rest.png", UriKind.Relative));
            //appBarButtonInfo.Click += appBarButtonInfo_Click;
            //appBarButtonInfo.Text = AppResources.Info;
            //#endregion

            //ApplicationBar.Buttons.Add(appBarButtonEmpty);
            ApplicationBar.Buttons.Add(appBarButtonAddAll);
            ApplicationBar.Buttons.Add(appBarButtonAddSelected);
        }

        void appBarButtonAddSelected_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in App.Current.FavouritesViewModel.ListOfItems)
            {
                if (!item.IsChecked) continue;
                App.Current.ViewModel.AddItem(item.Text);
            }
        }

        void appBarButtonAddAll_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in App.Current.FavouritesViewModel.ListOfItems)
            {
                App.Current.ViewModel.AddItem(item.Text);
            }
        }

        //void appBarButtonFavs_Click(object sender, EventArgs e)
        //{
        //    this.NavigationService.Navigate(new Uri("/src/views/FavouritesPage.xaml", UriKind.Relative));
        //}

        //void appBarButtonInfo_Click(object sender, EventArgs e)
        //{
        //    this.NavigationService.Navigate(new Uri("/src/views/InfoPage.xaml", UriKind.Relative));
        //}

        //void appBarButtonEmpty_Click(object sender, EventArgs e)
        //{
        //    App.Current.ViewModel.ClearList();
        //}
        #endregion
    }
}