using Application.Dto.Contacts;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.People
{
    public class PersonDto : IMap
    {
        public int PersonId { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public int FamilyId { get; set; }
        public Family Family { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonDto>();
        }
    }
}
