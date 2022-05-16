using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFamilyRepository
    {
        IEnumerable<Family> GetAll();
        IEnumerable<EducationalInstitution> GetAllEducInstitutions(int familyId);
        Family GetById(int id);
        EducationalInstitution GetInstitutionById(int id);
        Family GetByUserId(string userId);
        Family Add(Family family);
        EducationalInstitution AddInstitution(EducationalInstitution institution);
        void Update(Family family);
        void UpdateInstitution(EducationalInstitution institution);
        void Delete(Family family);
        bool AddPersonToFamily(int familyId, int personId);
    }
}
