using System;

namespace ChildSchedulerAPI.Dtos
{
    public record CreateProductDto
    {

        public string ProductName { get; init; }
        public decimal Price { get; init; }

    }

}