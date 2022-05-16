using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventType : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]    
        public string EventTypeName { get; set; }
    }
}
