using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Children
{
    public class UpdateChildDto : IMap
    {
        public int Id { get; set; }
        public string ChildName { get; set; }
        public int FamilyId { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[]? Picture { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<ChildHistory> ChildHistories { get; set; }
        public ICollection<EducationalInstitution> EducationalInstitutions { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateChildDto, Child>();
        }
    }
}
