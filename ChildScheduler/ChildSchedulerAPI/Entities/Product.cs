using System;

namespace ChildSchedulerAPI.Entities
{
    public record Product
    {
        public Guid Id { get; init; }

        public string ProductName { get; init; }
        public decimal Price { get; init; }
    }

}