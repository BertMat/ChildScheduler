using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly SchedulerContext _context;

        public ContactRepository(SchedulerContext context)
        {
            _context = context;
        }

        public IEnumerable<Contact> GetAll(string userId)
        {
            return _context.Contacts.Where(p => p.UserId == userId);
        }

        public Contact GetById(int id)
        {
            return _context.Contacts.SingleOrDefault(p => p.Id == id);
        }
        public Contact Add(Contact Contact)
        {
            _context.Contacts.Add(Contact);
            _context.SaveChanges();
            return Contact;
        }

        public void Update(Contact Contact)
        {
            _context.Contacts.Update(Contact);
            _context.SaveChanges();
        }
        public void Delete(Contact Contact)
        {
            _context.Remove(Contact);
            _context.SaveChanges();
        }

    }
}
