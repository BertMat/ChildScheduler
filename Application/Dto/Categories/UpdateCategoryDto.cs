using Application.Dto.People;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Categories
{
    public class UpdateCategoryDto : IMap
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int FamilyId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
