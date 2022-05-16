using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChildSchedulerAPI.Entities
{
    public record User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid UserId { get; init; }
        [EmailAddress]
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}