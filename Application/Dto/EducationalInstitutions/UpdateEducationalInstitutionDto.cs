using Application.Dto.Children;
using Application.Dto.Events;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.EducationalInstitutions
{
    public class UpdateEducationalInstitutionDto : IMap
    {
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public ICollection<ChildDto> Children { get; set; }
        public ICollection<EventDto> Events { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateEducationalInstitutionDto, EducationalInstitution>();
        }
    }
}
