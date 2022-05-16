using Application.Dto.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContactService
    {
        IEnumerable<ContactDto> GetAllContacts(string userId);
        ContactDto GetContactById(int id);
        ContactDto AddNewContact(CreateContactDto Contact);
        void UpdateContact(UpdateContactDto Contact);
        void DeleteContact(int id);
    }
}
