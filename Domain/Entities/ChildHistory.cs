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
    public class ChildHistory : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Child))]
        public int ChildId { get; set; }
        public Child Child { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
    }
}
