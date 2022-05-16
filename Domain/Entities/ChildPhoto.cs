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
    public class ChildPhoto : AuditableEntity
    {
        [Key]
        public int ChildPhotoId { get; set; }
        [Required]
        [ForeignKey(nameof(Child))]
        public int ChildId { get; set; }
        public Child Child{ get; set; }
        public byte[] Photo { get; set; }
        public string ChildPhotoDescription { get; set; }
    }
}
