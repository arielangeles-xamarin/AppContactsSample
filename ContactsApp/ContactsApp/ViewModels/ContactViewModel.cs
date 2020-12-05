using ContactsApp.Models;
using ContactsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        public string ContactTitle => "Contacts";
        public string AddText => "Add";

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand EditCommand { get; }

        public ObservableCollection<Contact> Contacts { get; set; }

        private Contact contactSelected;
        public Contact ContactSelected
        {
            get { return contactSelected; }
            set
            {
                if (!(value is null))
                    DisplayContactSelected();
       
                contactSelected = value;
            }
        }
        public ContactViewModel(ObservableCollection<Contact> contacts)
        {
            AddCommand = new Command(AddContact);
            EditCommand = new Command(EditContact);
            RemoveCommand = new Command(RemoveContact);
        }

        private async void AddContact()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddContactPage(Contacts));
        }

        private async void RemoveContact()
        {
            if(Contacts.Remove(contactSelected))
                await Application.Current.MainPage.DisplayAlert("Deleted", 
                    $"Contact: {contactSelected.Name} has been deleted successfully", "OK");
        }

        private async void EditContact()
        {
            Contacts.Remove(contactSelected);
            await Application.Current.MainPage.Navigation.PushAsync(new AddContactPage(Contacts));
        }

        public async void DisplayContactSelected()
        {
            const string Call = "Call";
            const string Edit = "Edit";
            const string Detail = "More info";
            
            string Cancel = "Cancel";
            string Title = "Choose an option";

            string option = await Application.Current.MainPage.DisplayActionSheet(Title, Cancel, null, Call, Edit, Detail);
            
            switch (option)
            {
                case Call:
                    MakePhoneCall(contactSelected.PhoneNumber);
                    break;
                case Edit:
                    EditContact();
                    break;
                case Detail:
                    DisplayDetail(contactSelected);
                    break;
                default:
                    break;
            }
  
        }

        private async void DisplayDetail(Contact selectedContact)
        {
            await Application.Current.MainPage.DisplayAlert("Contact info",
                $"Name: {selectedContact.Name}\n" +
                $"Number: {selectedContact.PhoneNumber}\n" +
                $"Mail: {selectedContact.Email}\n"+
                $"Address: {selectedContact.Address}", 
                "OK");
        }

        private void MakePhoneCall(string phoneNumber)
        {
            try
            {
                PhoneDialer.Open(phoneNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Catch any errors ocurred
            }
        }


    }
}
