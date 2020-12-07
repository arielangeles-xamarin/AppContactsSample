using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

       // protected override async void OnAppearing()
       // {
        //    base.OnAppearing();
        //
         //   contacts.ItemsSource = await App.Database.GetContactsAsync();
        //}
    }
}