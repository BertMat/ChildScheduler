using Application.Dto.EducationalInstitutions;
using Application.Dto.Families;
using Application.Dto.People;
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
    public class FamilyService : IFamilyService
    {
        public FamilyService(IFamilyRepository familyRepository, IMapper mapper)
        {
            _familyRepository = familyRepository;
            _mapper = mapper;
        }

        public readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;

        public FamilyDto AddNewFamily(CreateFamilyDto newFamily)
        {
            var family = _mapper.Map<Family>(newFamily);
            _familyRepository.Add(family);

            return _mapper.Map<FamilyDto>(family);
        }


        public void DeleteFamily(int id)
        {

            var existingFamily = _familyRepository.GetById(id);

            _familyRepository.Delete(existingFamily);
        }

        public IEnumerable<FamilyDto> GetAllFamilies()
        {
            var data = _familyRepository.GetAll();
            return _mapper.Map<IEnumerable<FamilyDto>>(data);
        }

        public FamilyDto GetFamilyById(int id)
        {
            var data = _familyRepository.GetById(id);
            return _mapper.Map<FamilyDto>(data);
        }

        public void UpdateFamily(UpdateFamilyDto updateFamily)
        {
            var existingFamily = _familyRepository.GetById(updateFamily.FamilyId);

            var family = _mapper.Map(updateFamily, existingFamily);
            _familyRepository.Update(family);

        }

        public bool AddPersonToFamily(int familyId, int personId)
        {
            return _familyRepository.AddPersonToFamily(familyId, personId);
        }

        public FamilyDto GetFamilyByUserId(string userId)
        {
            var data = _familyRepository.GetByUserId(userId);
            return _mapper.Map<FamilyDto>(data);
        }

        public IEnumerable<EducationalInstitutionDto> GetAllInstitutions(int familyId)
        {
            var data = _familyRepository.GetAllEducInstitutions(familyId);
            return _mapper.Map<IEnumerable<EducationalInstitutionDto>>(data);
        }

        public EducationalInstitutionDto GetInsitutionById(int id)
        {
            var data = _familyRepository.GetInstitutionById(id);
            return _mapper.Map<EducationalInstitutionDto>(data);
        }

        public EducationalInstitutionDto AddNewInstitution(CreateEducationalInstitutionDto institution)
        {
            var newInstitution = _mapper.Map<EducationalInstitution>(institution);
            _familyRepository.AddInstitution(newInstitution);

            return _mapper.Map<EducationalInstitutionDto>(newInstitution);
        }

        public void UpdateInstitution(UpdateEducationalInstitutionDto institution)
        {
            var existingInstitution = _familyRepository.GetInstitutionById(institution.Id);

            var insti = _mapper.Map(institution, existingInstitution);
            _familyRepository.UpdateInstitution(insti);
        }
    }
}
