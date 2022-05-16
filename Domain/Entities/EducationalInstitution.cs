using Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class EducationalInstitution : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Family))]
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public ICollection<Child> Children { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}