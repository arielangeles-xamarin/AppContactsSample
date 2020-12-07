using ContactsApp.Models;
using ContactsApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
    public class ContactViewModel : INotifyPropertyChanged
    {
        public string ContactTitle => "Contacts";

        public ICommand NewCommand { get; set; }
        public ICommand DeleteContactCommand { get; set; }
        public ICommand EditContactCommand { get; set; }

        public ObservableCollection<Contact> Contacts { get; set; }

        public IList<Contact> ContactList { get; set; }

        private Contact contactSelected;
        public Contact ContactSelected
        {
            get { return contactSelected; }
            set
            {
                if (value != null)
                {
                    contactSelected = value;
                    DisplayContactSelected();
                }
            }
        }

        public ContactViewModel()
        {
            NewCommand = new Command(async () => await ExecuteNewCommand());
            DeleteContactCommand = new Command<Contact>(async (Contact contact) => await RemoveContact());
            EditContactCommand = new Command<Contact>(async (Contact contact) => await EditContact());

            Task.Run(async () => { ContactList = await App.Database.GetContactsAsync(); }).Wait();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task ExecuteNewCommand() => await Application.Current.MainPage.Navigation.PushAsync(new AddContactPage());

        private async Task RemoveContact()
        {
            await App.Database.DeleteContactAsync(ContactSelected);
            await Application.Current.MainPage.DisplayAlert("Deleted",
                $"Contact: {contactSelected.FullName} has been deleted successfully", "OK");

        }

        private async Task EditContact()
        {
            await App.Database.DeleteContactAsync(ContactSelected);
            await Application.Current.MainPage.Navigation.PushAsync(new AddContactPage(ContactSelected));
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
                    MakePhoneCall(contactSelected.Phone);
                    break;
                case Edit:
                    await EditContact();
                    break;
                case Detail:
                    await DisplayDetail(contactSelected);
                    break;
                default:
                    break;
            }

        }

        private async Task DisplayDetail(Contact selectedContact)
        {
            await Application.Current.MainPage.DisplayAlert("Contact info",
                $"Name: {selectedContact.FullName}\n" +
                $"Number: {selectedContact.Phone}\n" +
                $"Mail: {selectedContact.Email}",
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
                Debug.WriteLine(ex.Message); // Catch any errors ocurred
            }
        }
    }
}
