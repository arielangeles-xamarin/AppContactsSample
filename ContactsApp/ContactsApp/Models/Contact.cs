using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SQLite;

namespace ContactsApp.Models 
{
    public class Contact : INotifyPropertyChanged
    {
        public Contact()
        {

        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
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
