using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ContactsApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
