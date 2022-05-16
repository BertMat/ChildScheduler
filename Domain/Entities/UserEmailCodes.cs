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
    public class UserEmailCodes : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(IdentityUser))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string InviteKey { get; set; }
    }
}
