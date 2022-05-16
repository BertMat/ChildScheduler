using Application.Dto.Categories;
using Application.Dto.Children;
using Application.Dto.Costs;
using Application.Dto.EducationalInstitutions;
using Application.Dto.EventPhotos;
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
    public class EventDto : IMap
    {
        public int Id { get; set; }
        public int EventTypeId { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? EducationalInstitutionId { get; set; }
        public EducationalInstitutionDto? EducationalInstitution { get; set; }
        public ICollection<ChildDto> Children { get; set; }
        public ICollection<PersonDto> People { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventDto>();
        }
    }
}
