﻿using System.Runtime.Serialization;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ListaSpesa.Viewmodels;
using Microsoft.Phone.Controls;

namespace ListaSpesa.Model
{
    [DataContract]
    public class ListItem : ViewModelBase
    {
        #region Constructors
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
        }
        #endregion

        #region Properties

        #region IsChecked

        private const string _isCheckedPrpName = "IsChecked";
        private bool _isChecked;

        [DataMember]
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged(_isCheckedPrpName);
            }
        }
        #endregion

        #region Text
        private const string _textPrpName = "Text";
        private string _text;

        [DataMember]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged(_textPrpName);
            }
        }
        #endregion
        #endregion


    }
}
