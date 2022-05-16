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
    public class FamilyRepository : IFamilyRepository
    {
        private readonly SchedulerContext _context;

        public FamilyRepository(SchedulerContext context)
        {
            _context = context;
        }

        public IEnumerable<Family> GetAll()
        {
            return _context.Families;
        }

        public Family GetById(int id)
        {
            return _context.Families.SingleOrDefault(p => p.FamilyId == id);
        }
        public Family Add(Family family)
        {
            _context.Families.Add(family);
            _context.SaveChanges();
            return family;
        }

        public void Update(Family family)
        {
            _context.Families.Update(family);
            _context.SaveChanges();
        }
        public void UpdateInstitution(EducationalInstitution institution)
        {
            _context.EducationalInstitutions.Update(institution);
            _context.SaveChanges();
        }
        public void Delete(Family family)
        {
            _context.Remove(family);
            _context.SaveChanges();
        }
        public bool AddPersonToFamily(int familyId, int personId)
        {
            var family = _context.Families.SingleOrDefault(p => p.FamilyId == familyId);
            var person = _context.People.SingleOrDefault(p => p.PersonId == personId);
            if(family != null && person != null && !person.FamilyId.HasValue)
            {
                person.FamilyId = familyId;
                _context.People.Update(person);
                _context.SaveChanges();
                return true;
            }

            return false;

        }

        public Family GetByUserId(string userId)
        {
            return _context.Families.SingleOrDefault(p => p.UserId == userId);
        }

        public IEnumerable<EducationalInstitution> GetAllEducInstitutions(int familyId)
        {
            return _context.EducationalInstitutions.Where(p => p.FamilyId == familyId);
        }

        public EducationalInstitution GetInstitutionById(int id)
        {
            return _context.EducationalInstitutions.SingleOrDefault(p => p.Id == id);
        }

        public EducationalInstitution AddInstitution(EducationalInstitution institution)
        {
            _context.EducationalInstitutions.Add(institution);
            _context.SaveChanges();
            return institution;
        }
    }
}
