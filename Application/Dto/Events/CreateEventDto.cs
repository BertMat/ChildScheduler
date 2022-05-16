using Application.Dto.Children;
using Application.Dto.Contacts;
using Application.Dto.Costs;
using Application.Dto.EducationalInstitutions;
using Application.Dto.People;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Events
{
    public class CreateEventDto : IMap
    {
        public int EventTypeId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int FamilyId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? EducationalInstitutionId { get; set; }
        public ICollection<Child> Children { get; set; }
        public ICollection<Person> People { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Cost> Costs { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateEventDto, Event>();
        }
    }
}
