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
    public class Family : AuditableEntity
    {
        [Key]
        public int FamilyId { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId{ get; set; }
        public IdentityUser User { get; set; }
        public string FamilyName { get; set; }
        public ICollection<Person> People { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
