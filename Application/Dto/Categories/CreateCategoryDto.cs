using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Categories
{
    public class CreateCategoryDto : IMap
    {
        public string CategoryName { get; set; }
        public int FamilyId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCategoryDto, Category>();
        }
    }
}
