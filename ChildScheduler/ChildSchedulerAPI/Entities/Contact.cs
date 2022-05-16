using System;
using System.Collections.Generic;

namespace ChildSchedulerAPI.Entities
{
    public record Contact
    {
        public Guid Id { get; set; }
        public string ContactAlias { get; set; }
        public string ContactName { get; set; }
        public string ContactSurname { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<SocialMedia> SocialMedias { get; set; }        
    }
}