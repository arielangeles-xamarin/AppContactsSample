using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsApp.ViewModels
{
	public class AddContactViewModel : BaseViewModel
	{
		public ICommand AddCommand { get; }
		public string DefaultImage => "DefaultImage.png";

		private Contact contact;
		public Contact NewContact
		{
			get
			{
				return contact;
			}
			set
			{
				if (!(value is null)) {
					contact = value;
				}

			}
		}

		public AddContactViewModel(ObservableCollection<Contact> contacts)
		{
			NewContact = new Contact() {
				ProfilePicture = DefaultImage
			};

			AddCommand = new Command(async () =>
			{
				contacts.Add(new Contact {
					Name = NewContact.Name,
					PhoneNumber = NewContact.PhoneNumber,
					Email = NewContact.Email,
					Address = NewContact.Address,
					ProfilePicture = NewContact.ProfilePicture
				});

				await Application.Current.MainPage.Navigation.PopAsync();
			});
		}

		public AddContactViewModel(ObservableCollection<Contact> contacts, Contact contactSelected)
		{
			NewContact = contactSelected;
			AddCommand = new Command(async () => {
				contacts.Add(contactSelected);
				await App.Current.MainPage.Navigation.PopAsync();
			});
		}
		

	}
}
