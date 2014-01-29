using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using ListaSpesa.Model;
using ListaSpesa.Utils;
using ListaSpesa.Viewmodel;

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

        public MainPageViewModel Load()
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

        internal void Save(MainPageViewModel vm)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new IsolatedStorageFileStream(Constants.StorageFileName,
                                                                  FileMode.Create,
                                                                  FileAccess.Write,
                                                                  store))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ListItem>));
                    serializer.Serialize(stream, vm.Items);
                }
            }
        }
    }
}
