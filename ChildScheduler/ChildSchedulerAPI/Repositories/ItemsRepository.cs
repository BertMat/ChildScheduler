using System;
using System.Collections.Generic;
using ChildSchedulerAPI.Entities;

namespace ChildSchedulerAPI.Repositories
{
    public interface IItemsRepository
    {
        void CreateProduct(Product product);
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts();
    }
}