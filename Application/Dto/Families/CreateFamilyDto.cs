using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Families
{
    public class CreateFamilyDto : IMap
    {
        public string UserId { get; set; }
        public string FamilyName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFamilyDto, Family>();
        }
    }
}
