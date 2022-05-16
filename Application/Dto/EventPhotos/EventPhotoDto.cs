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

namespace Application.Dto.EventPhotos
{
    public class EventPhotoDto : IMap
    {
        public int EventPhotoId { get; set; }
        public int EventId { get; set; }
        public byte[] Photo { get; set; }
        public string? EventPhotoDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EventPhoto, EventPhotoDto>();
        }
    }
}
