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
    public class Child : AuditableEntity
    {
        public Child()
        {
            Events = new HashSet<Event>();
            ChildHistories = new HashSet<ChildHistory>();
            EducationalInstitutions = new HashSet<EducationalInstitution>();
            Photos = new HashSet<ChildPhoto>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string ChildName { get; set; }
        public byte[]? Picture { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        [ForeignKey(nameof(Family))]
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<ChildHistory> ChildHistories { get; set; }
        public ICollection<EducationalInstitution> EducationalInstitutions { get; set; }
        public ICollection<ChildPhoto> Photos { get; set; }
    }
}
