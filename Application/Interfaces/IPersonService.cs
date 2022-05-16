using Application.Dto.Contacts;
using Application.Dto.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonDto> GetAllPeople();
        PersonDto GetPersonById(int id);
        PersonDto GetPersonByUser(string userId);
        PersonDto AddNewPerson(CreatePersonDto person);
        void UpdatePerson(UpdatePersonDto person);
        void DeletePerson(int id);
        IEnumerable<PersonDto> GetFamilyMembers(int personId);
    }
}
