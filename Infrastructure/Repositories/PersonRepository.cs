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
    public class PersonRepository : IPersonRepository
    {
        private readonly SchedulerContext _context;

        public PersonRepository(SchedulerContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.People;
        }

        public Person GetById(int id)
        {
            return _context.People.SingleOrDefault(p => p.PersonId == id);
        }
        public Person GetByUser(string userId)
        {
            return _context.People.SingleOrDefault(p => p.UserId == userId);
        }
        public Person Add(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
            return person;
        }

        public void Update(Person person)
        {
            _context.People.Update(person);
            _context.SaveChanges();
        }
        public void Delete(Person person)
        {
            _context.Remove(person);
            _context.SaveChanges();
        }

    }
}
