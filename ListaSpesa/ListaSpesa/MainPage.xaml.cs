using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using System.Xml.Serialization;
using ListaSpesa.Model;
using ListaSpesa.Viewmodels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ListaSpesa.Resources;
using Mangopollo.Tiles;
using System.Diagnostics;
using ListaSpesa.Utils;

namespace ListaSpesa
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            CreateTiles();

            this.DataContext = App.Current.ViewModel;

            FeedbackOverlay.VisibilityChanged += FeedbackOverlay_VisibilityChanged;
        }

        private void CreateTiles()
        {
            if (Mangopollo.Utils.CanUseLiveTiles)
            {
                var tileId = ShellTile.ActiveTiles.FirstOrDefault();
                if (tileId != null)
                {
                    var tileData = new FlipTileData();
                    tileData.Title = AppResources.ApplicationTitle;
                    tileData.BackContent = "";
                    //tileData.BackgroundImage = new Uri("/Icons/173x173.png", UriKind.Relative);
                    tileData.BackBackgroundImage = new Uri("/Icons/173x173.png", UriKind.Relative);
                    tileData.WideBackContent = "";
                    //tileData.WideBackgroundImage = new Uri("/Icons/346x173.png", UriKind.Relative);
                    tileData.WideBackBackgroundImage = new Uri("/Icons/346x173.png", UriKind.Relative);
                    Debug.WriteLine("Activating live tile: " + Mangopollo.Utils.CanUseLiveTiles);
                    tileId.Update(tileData);
                }
            }
        }

        private void FeedbackOverlay_VisibilityChanged(object sender, EventArgs e)
        {
            ApplicationBar.IsVisible = (FeedbackOverlay.Visibility != Visibility.Visible);
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox textBox = sender as TextBox;
                // Update the binding source
                BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);
                if(bindingExpr!=null)
                {
                    bindingExpr.UpdateSource();
                }
            }
        }

        #region Context Menu

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedListBoxItem = this.listBox1.ItemContainerGenerator.ContainerFromItem(
                (sender as MenuItem).DataContext) as ListBoxItem;
            if (selectedListBoxItem == null)
            {
                return;
            }

            if (selectedListBoxItem.DataContext is ListItem)
            {
                App.Current.ViewModel.RemoveItem(selectedListBoxItem.DataContext as ListItem);
            }
        }

        #endregion

        #region ApplicationBar

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            #region Empty
            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButtonEmpty =
                new ApplicationBarIconButton(new
                Uri("/Images/appbar.cancel.rest.png", UriKind.Relative));
            appBarButtonEmpty.Click += appBarButtonEmpty_Click;
            appBarButtonEmpty.Text = AppResources.Svuota;
            #endregion

            #region Favourites
            ApplicationBarIconButton appBarButtonFavs =
                 new ApplicationBarIconButton(new Uri("/Images/appbar.favs.rest.png", UriKind.Relative));
            appBarButtonFavs.Click += appBarButtonFavs_Click;
            appBarButtonFavs.Text = AppResources.Favourites;
            #endregion

            #region Info
            ApplicationBarIconButton appBarButtonInfo =
                new ApplicationBarIconButton(new Uri("/Images/appbar.questionmark.rest.png", UriKind.Relative));
            appBarButtonInfo.Click += appBarButtonInfo_Click;
            appBarButtonInfo.Text = AppResources.Info;
            #endregion

            ApplicationBar.Buttons.Add(appBarButtonEmpty);
            ApplicationBar.Buttons.Add(appBarButtonFavs);
            ApplicationBar.Buttons.Add(appBarButtonInfo);
        }

        void appBarButtonFavs_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/src/views/FavouritesPage.xaml", UriKind.Relative));
        }

        void appBarButtonInfo_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/src/views/InfoPage.xaml", UriKind.Relative));
        }

        void appBarButtonEmpty_Click(object sender, EventArgs e)
        {
            App.Current.ViewModel.ClearList();
        }
        #endregion

    }
}