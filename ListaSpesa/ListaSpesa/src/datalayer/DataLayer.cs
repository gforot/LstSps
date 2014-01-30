using System;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using ListaSpesa.Model;
using ListaSpesa.Utils;
using ListaSpesa.Viewmodels;

namespace ListaSpesa.Datalayer
{
    public class DataLayer
    {
        private static DataLayer _instance;

        public static DataLayer GetInstance()
        {
            return _instance ?? (_instance = new DataLayer());
        }

        private DataLayer()
        {

        }

        public ObservableCollection<ListItem> LoadSpesa()
        {
            ObservableCollection<ListItem> items = new ObservableCollection<ListItem>();
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(Constants.StorageFileName))
                {
                    using (IsolatedStorageFileStream isfs = isf.OpenFile(Constants.StorageFileName, FileMode.Open))
                    {
                        Type t = typeof(ObservableCollection<ListItem>);
                        XmlSerializer ser = new XmlSerializer(t);
                        object obj = ser.Deserialize(isfs);

                        if ((obj != null) && (obj is ObservableCollection<ListItem>))
                        {
                           items = obj as ObservableCollection<ListItem>;
                        }
                    }
                }
            }
            return items;
        }

        public void SaveSpesa(ObservableCollection<ListItem> items)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream(Constants.StorageFileName,
                                                                  FileMode.Create,
                                                                  FileAccess.Write,
                                                                  store))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ListItem>));
                    serializer.Serialize(stream, items);
                }
            }
        }

        public ObservableCollection<ListItem> LoadFavourites()
        {
            ObservableCollection<ListItem> rval = new ObservableCollection<ListItem>();
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(Constants.FavouritesFileName))
                {
                    using (IsolatedStorageFileStream isfs = isf.OpenFile(Constants.FavouritesFileName, FileMode.Open))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<ListItem>));
                        object obj = ser.Deserialize(isfs);

                        if ((obj != null) && (obj is ObservableCollection<ListItem>))
                        {
                            rval = obj as ObservableCollection<ListItem>;
                        }
                    }
                }
            }
            return rval;
        }

        public void SaveFavourites(ObservableCollection<ListItem> items)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream(Constants.FavouritesFileName,
                                                                  FileMode.Create,
                                                                  FileAccess.Write,
                                                                  store))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ListItem>));
                    serializer.Serialize(stream, items);
                }
            }
        }
    }
}
