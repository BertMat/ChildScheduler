using Application.Dto.EducationalInstitutions;
using Application.Dto.Families;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFamilyService
    {
        IEnumerable<FamilyDto> GetAllFamilies();
        IEnumerable<EducationalInstitutionDto> GetAllInstitutions(int familyId);
        FamilyDto GetFamilyById(int id);
        EducationalInstitutionDto GetInsitutionById(int id);
        FamilyDto GetFamilyByUserId(string userId);
        FamilyDto AddNewFamily(CreateFamilyDto family);
        EducationalInstitutionDto AddNewInstitution(CreateEducationalInstitutionDto institution);
        bool AddPersonToFamily(int familyId, int personId);
        void UpdateFamily(UpdateFamilyDto family);
        void UpdateInstitution(UpdateEducationalInstitutionDto institution);
        void DeleteFamily(int id);
    }
}
