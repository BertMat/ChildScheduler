using System;

namespace ChildSchedulerAPI.Dtos
{
    public record ProductDto
    {
        public Guid Id { get; init; }

        public string ProductName { get; init; }
        public decimal Price { get; init; }

    }

}