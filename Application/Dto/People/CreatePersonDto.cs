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
    public class CreatePersonDto : IMap
    {
        public string UserId { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePersonDto, Person>();
        }
    }
}
