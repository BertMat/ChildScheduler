using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.People
{
    public class UpdatePersonDto : IMap
    {
        public int PersonId { get; set; }
        public string UserId { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePersonDto, Person>();
        }
    }
}
