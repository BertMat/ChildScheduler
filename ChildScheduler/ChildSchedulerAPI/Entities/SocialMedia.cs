using System;

namespace ChildSchedulerAPI.Entities
{
    public record SocialMedia
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SocialMediaUrl { get; set; }
        public Contact Contact { get; set; }
        public Guid ContactId { get; set; }
    }
}