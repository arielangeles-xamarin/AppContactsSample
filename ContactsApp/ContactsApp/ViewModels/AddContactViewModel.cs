using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class AddContactViewModel : INotifyPropertyChanged
    {
        public string AddContactTitle => "Add Contact";

        public Contact Contact { get; set; }
        public ObservableCollection<Contact> Contacts { get; set; }

        public ICommand SaveContactCommand { get; set; }

        public AddContactViewModel(ObservableCollection<Contact> contacts, Contact contact)
        {           
            Contact = new Contact();
            
            SaveContactCommand = new Command(async () =>
            {

                contacts.Add(new Contact(
                    Contact.FirstName,
                    Contact.LastName,
                    Contact.Phone,
                    Contact.Email
                ));

                await Application.Current.MainPage.DisplayAlert("New Contact Added",
                    $"Contact {Contact.FullName} has been successfully added", "Ok");

                await Application.Current.MainPage.Navigation.PopAsync();
            });
        }

        public AddContactViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
