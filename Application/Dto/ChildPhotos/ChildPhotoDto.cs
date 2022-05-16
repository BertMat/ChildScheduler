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

namespace Application.Dto.ChildPhotos
{
    public class ChildPhotoDto : IMap
    {
        public int ChildPhotoId { get; set; }
        public int ChildId { get; set; }
        public Child Child { get; set; }
        public byte[] Photo { get; set; }
        public string ChildPhotoDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChildPhoto, ChildPhotoDto>();
        }
    }
}
