using ChildSchedulerAPI.Dtos;
using ChildSchedulerAPI.Entities;

namespace ChildSchedulerAPI
{
    public static class Extensions
    {
        public static ProductDto AsDto(this Product item)
        {
            return new ProductDto
            {
                Id = item.Id,
                ProductName = item.ProductName,
                Price = item.Price,
            };
        }
    }
}