using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ContactsApp.Models;
using System.Threading.Tasks;

namespace ContactsApp.Data
{
    public class ContactDatabase
    {
        readonly SQLiteAsyncConnection db;

        public ContactDatabase(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Contact>().Wait();
        }

        public Task<List<Contact>> GetContactsAsync()
        {
            return db.Table<Contact>().ToListAsync();
        }

        public Task<Contact> GetContactAsync(int id)
        {
            return db.Table<Contact>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveContactAsync(Contact contact)
        {
            if (contact.Id > 0)
            {
                return db.UpdateAsync(contact);
            }
            else
            {
                return db.InsertAsync(contact);
            }
        }

        public Task<int> DeleteContactAsync(Contact contact)
        {
            return db.DeleteAsync(contact);
        }
    }

}

