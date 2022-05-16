using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FamilyInvites : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Family))]
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public string FamilyName { get; set; }
        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string InviteKey { get; set; }
    }
}
