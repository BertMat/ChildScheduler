using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Contacts
{
    public class ContactDto : IMap
    {
        public int Id { get; set; }
        public string ContactAlias { get; set; }
        public string ContactName { get; set; }
        public string ContactSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string UserId { get; set; }
        public IEnumerable<SocialMedia> SocialMedias { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactDto>();
        }
    }
}
