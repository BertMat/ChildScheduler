using Application.Dto.People;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services
{
    public class PersonService : IPersonService
    {

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public IEnumerable<PersonDto> GetAllPeople()
        {
            var data = _personRepository.GetAll();
            return _mapper.Map<IEnumerable<PersonDto>>(data);
        }

        public IEnumerable<PersonDto> GetFamilyMembers(int personId)
        {
            var person = _personRepository.GetById(personId);
            var data = _personRepository.GetAll();
            var list = (_mapper.Map<IEnumerable<PersonDto>>(data)).Where(p => p.FamilyId == person.FamilyId).ToList();

            return list;
        }

        public PersonDto GetPersonById(int id)
        {
            var data = _personRepository.GetById(id);
            return _mapper.Map<PersonDto>(data);
        }

        public PersonDto AddNewPerson(CreatePersonDto newPerson)
        {

            var person = _mapper.Map<Person>(newPerson);
            _personRepository.Add(person);

            return _mapper.Map<PersonDto>(person);

        }
        public void UpdatePerson(UpdatePersonDto updatePerson)
        {

            var existingPerson = _personRepository.GetById(updatePerson.PersonId);


            var person = _mapper.Map(updatePerson, existingPerson);
            _personRepository.Update(person);

        }
        public void DeletePerson(int id)
        {

            var existingPerson = _personRepository.GetById(id);

            _personRepository.Delete(existingPerson);

        }

        public PersonDto GetPersonByUser(string userId)
        {
            var existringPerson = _personRepository.GetByUser(userId);

            return _mapper.Map<PersonDto>(existringPerson);
        }
    }
}
