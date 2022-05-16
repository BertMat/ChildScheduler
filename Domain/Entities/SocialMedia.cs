using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SocialMedia : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SocialMediaUrl { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
