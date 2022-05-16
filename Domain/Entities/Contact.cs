using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Contacts")]
    public class Contact : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string ContactAlias { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactSurname { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public IEnumerable<SocialMedia> SocialMedias { get; set; }
        public ICollection<Event> Events { get; set; }

        public Contact()
        {

        }
    }
}
