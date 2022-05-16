using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Dto.Families
{
    public class FamilyDto : IMap
    {
        public int FamilyId { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string FamilyName { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Child> Children { get; set; }
        public ICollection<Person> FamilyMembers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Family, FamilyDto>();
        }
    }
}
