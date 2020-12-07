using ContactsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactsApp.Extensions
{
    public static class ModelExtensions
    {
        public static Contact Clone(this Contact o) =>
            new Contact {
                Id = o.Id,
                Email = o.Email,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Phone = o.Phone
            };
    }
}
