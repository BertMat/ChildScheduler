using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll(string userId);
        Contact GetById(int id);
        Contact Add(Contact contact);
        void Update(Contact contact);
        void Delete(Contact contact);
    }
}
