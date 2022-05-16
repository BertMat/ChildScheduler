using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person : AuditableEntity
    {
        public Person()
        {
            Events = new HashSet<Event>();
        }
        public int PersonId { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public int? FamilyId { get; set; }
        public Family Family { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
