using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventPhoto : AuditableEntity
    {
        public int EventPhotoId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public byte[] Photo { get; set; }
        public string? EventPhotoDescription { get; set; }
    }
}
