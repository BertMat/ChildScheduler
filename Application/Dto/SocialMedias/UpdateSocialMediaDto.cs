using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.SocialMedias
{
    public class UpdateSocialMediaDto : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SocialMediaUrl { get; set; }
        public int ContactId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSocialMediaDto, SocialMedia>();
        }
    }
}
