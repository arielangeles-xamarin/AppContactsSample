using ContactsApp.Extensions;
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
        private readonly bool isNew;

        public string ContactTitle { get; }

        public Contact Contact { get; set; }
        public ObservableCollection<Contact> Contacts { get; set; }

        public ICommand SaveContactCommand { get; set; }
        
        public AddContactViewModel()
        {
            Contact = new Contact();
            isNew = true;
            ContactTitle = "New Contact";
        }

        public AddContactViewModel(Contact contact)
        {
            if (contact == null)
            {
                Contact = new Contact();
                isNew = true;
                ContactTitle = "New Contact";
            }
            else
            {
                Contact = contact.Clone();
                ContactTitle = "Edit Contact";
            }
           
            
            SaveContactCommand = new Command(async () =>
            {
                if (string.IsNullOrWhiteSpace(Contact.LastName) || string.IsNullOrWhiteSpace(Contact.FirstName))
                {
                    await Application.Current.MainPage.DisplayAlert("Invalid Name", "Name Fields cannot be empty", "Ok");
                    return;
                }

                if (isNew)
                {
                    await App.Database.SaveContactAsync(Contact);

                    await App.Database.GetContactsAsync();

                    await Application.Current.MainPage.DisplayAlert("New Contact Added",
                    $"Contact {Contact.FullName} has been successfully added", "Ok");
                }
                else
                {
                    await App.Database.SaveContactAsync(Contact);
                }

                await Application.Current.MainPage.Navigation.PopAsync();
            });
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
