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
    public class Category : AuditableEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required]
        [ForeignKey(nameof(Family))]
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
