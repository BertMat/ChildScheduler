using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChildSchedulerAPI.Entities
{
    public record Family
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public string FamilyName { get; set; }
        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
    }
}