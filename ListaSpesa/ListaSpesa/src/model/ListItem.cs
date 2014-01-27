
using System.ComponentModel;
using System.Runtime.Serialization;

namespace ListaSpesa.Model
{
    [DataContract]
    public class ListItem : INotifyPropertyChanged
    {
        private static int _index = 1;

        public ListItem()
            :this(string.Empty)
        {
            
        }

        public ListItem(string text)
            :this(text, false)
        {
        }

        public ListItem(string text, bool isChecked)
        {
            Text = text;
            _isChecked = isChecked;
            _id = _index++;
        }

        private bool _isChecked;
        private string _text;

        private int _id;
        public int Id
        {
            get { return _id; }
        }

        [DataMember]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }
        
        [DataMember]
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string prpName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prpName));
            }
        }
    }
}
