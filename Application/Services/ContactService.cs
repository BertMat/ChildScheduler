using Application.Dto.Contacts;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContactService : IContactService
    {

        public ContactService(IContactRepository ContactRepository, IMapper mapper)
        {
            _ContactRepository = ContactRepository;
            _mapper = mapper;
        }

        public readonly IContactRepository _ContactRepository;
        private readonly IMapper _mapper;

        public IEnumerable<ContactDto> GetAllContacts(string userId)
        {
            var data = _ContactRepository.GetAll(userId);
            return _mapper.Map<IEnumerable<ContactDto>>(data);

        }

        public ContactDto GetContactById(int id)
        {
            var data = _ContactRepository.GetById(id);
            return _mapper.Map<ContactDto>(data);
        }

        public ContactDto AddNewContact(CreateContactDto newContact)
        {

            var contact = _mapper.Map<Contact>(newContact);
            _ContactRepository.Add(contact);

            return _mapper.Map<ContactDto>(contact);

        }
        public void UpdateContact(UpdateContactDto updateContact)
        {

            var existingContact = _ContactRepository.GetById(updateContact.Id);


            var Contact = _mapper.Map(updateContact, existingContact);
            _ContactRepository.Update(Contact);

        }
        public void DeleteContact(int id)
        {

            var existingContact = _ContactRepository.GetById(id);

            _ContactRepository.Delete(existingContact);

        }
    }
}
