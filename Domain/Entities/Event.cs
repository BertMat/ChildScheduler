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
    public class Event : AuditableEntity
    {
        public Event()
        {
            People = new HashSet<Person>();
            Contacts = new HashSet<Contact>();
            Children = new HashSet<Child>();
            Photos = new HashSet<EventPhoto>();
            Costs = new HashSet<Cost>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(EventType))]
        public int EventTypeId { get; set; }
        public EventType EventyType { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category{ get; set; }
        [ForeignKey(nameof(Family))]
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        [ForeignKey(nameof(EducationalInstitution))]
        public int? EducationalInstitutionId { get; set; }
        public EducationalInstitution? EducationalInstitution { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        public string EventDescription { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public ICollection<Person> People { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Child> Children {  get; set; }
        public ICollection<Cost> Costs {  get; set; }
        public ICollection<EventPhoto> Photos {  get; set; }
    }
}
