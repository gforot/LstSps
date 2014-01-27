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
using ListaSpesa.Viewmodel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ListaSpesa.Resources;
using Mangopollo.Tiles;
using System.Diagnostics;

namespace ListaSpesa
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static MainPageViewModel _vm;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            CreateTiles();

            if (_vm == null)
            {
                _vm = Load();
            }
            this.DataContext = _vm;

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

        void FeedbackOverlay_VisibilityChanged(object sender, EventArgs e)
        {
            ApplicationBar.IsVisible = (FeedbackOverlay.Visibility != Visibility.Visible);
        }


        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            _vm.ClearList();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Save(_vm);
        }

        private static void Save(MainPageViewModel viewModel)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream(Constants.StorageFileName,
                                                                  FileMode.Create,
                                                                  FileAccess.Write,
                                                                  store))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ListItem>));
                    serializer.Serialize(stream, viewModel.Items);
                }
            }
        }

        private static MainPageViewModel Load()
        {
            MainPageViewModel viewModel = new MainPageViewModel();
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(Constants.StorageFileName))
                {
                    using (IsolatedStorageFileStream isfs = isf.OpenFile(Constants.StorageFileName, FileMode.Open))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<ListItem>));
                        object obj = ser.Deserialize(isfs);

                        if ((obj != null) && (obj is ObservableCollection<ListItem>))
                        {
                            viewModel.SetItems(obj as ObservableCollection<ListItem>);
                        }
                    }
                }
            }
            return viewModel;
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

        //private void ShowTxtBox_Click(object sender, EventArgs e)
        //{
        //    _vm.ShowInput = true;
        //}

        //private void HideTxtBox_Click(object sender, EventArgs e)
        //{
        //    _vm.ShowInput = false;
        //}

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void Info_Click(object sender, EventArgs e)
        {
            //Click
            this.NavigationService.Navigate(new Uri("/InfoPage.xaml", UriKind.Relative));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedListBoxItem = this.listBox1.ItemContainerGenerator.ContainerFromItem(
                (sender as MenuItem).DataContext) as ListBoxItem;
            if (selectedListBoxItem == null)
            {
                return;
            }

            if (selectedListBoxItem.DataContext is ListItem)
            {
                _vm.RemoveItem(selectedListBoxItem.DataContext as ListItem);
            }
        }

        // Build a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButtonSvuota =
                new ApplicationBarIconButton(new
                Uri("/Images/appbar.cancel.rest.png", UriKind.Relative));
            appBarButtonSvuota.Click += ApplicationBarIconButton_Click;
            appBarButtonSvuota.Text = AppResources.Svuota;

            ApplicationBarIconButton appBarButtonSalva =
    new ApplicationBarIconButton(new
    Uri("/Images/appbar.save.rest.png", UriKind.Relative));
            appBarButtonSalva.Click += Save_Click;
            appBarButtonSalva.Text = AppResources.Salva;

            ApplicationBarIconButton appBarButtonInfo =
new ApplicationBarIconButton(new
Uri("/Images/appbar.questionmark.rest.png", UriKind.Relative));
            appBarButtonInfo.Click += Info_Click;
            appBarButtonInfo.Text = AppResources.Info;

            ApplicationBar.Buttons.Add(appBarButtonSvuota);
            ApplicationBar.Buttons.Add(appBarButtonSalva);
            ApplicationBar.Buttons.Add(appBarButtonInfo);
        }


    }
}