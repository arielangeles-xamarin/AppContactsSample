using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ContactsApp.Models 
{
    public class Contact : INotifyPropertyChanged
    {
        public Contact()
        {

        }
        public Contact(string firstName, string lastName, string phone, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
