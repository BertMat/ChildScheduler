using ChildSchedulerAPI.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ChildSchedulerAPI.Repositories
{

    public class InMemItemsRepository : IItemsRepository
    {
        public readonly List<Product> items = new()
        {
            new Product { Id = Guid.NewGuid(), ProductName = "Potion", Price = 9 },
            new Product { Id = Guid.NewGuid(), ProductName = "Iron Sword", Price = 20 },
            new Product { Id = Guid.NewGuid(), ProductName = "Leather Breastplate", Price = 30 }
        };

        public IEnumerable<Product> GetProducts()
        {
            return items;
        }

        public Product GetProduct(Guid id)
        {
            return items.Where(p => p.Id == id).FirstOrDefault();
        }

        public void CreateProduct(Product product)
        {
            items.Add(product);
        }
    }

}