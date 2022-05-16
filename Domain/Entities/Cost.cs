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
    public class Cost : AuditableEntity
    {
        [Key]
        public int CostId { get; set; }
        public string CostName { get; set; }
        public string CostDescription { get; set; }
        public decimal Value { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(Event))]
        public int? EventId { get; set; }
        public Event Event { get; set; }
        public DateTime CostDate { get; set; }
    }
}
