using System;
using System.Collections.Generic;
using System.Linq;
using ChildSchedulerAPI.Dtos;
using ChildSchedulerAPI.Entities;
using ChildSchedulerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ChildSchedulerAPI.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ProductsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // GET /items
        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            var items = repository.GetProducts().Select(p => p.AsDto());
            return items;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(Guid id)
        {
            var item = repository.GetProduct(id);
            if(item is null)
                return NotFound();
            return item.AsDto();
        }

        // POST /items/
        [HttpPost]
        public ActionResult<ProductDto> CreateProduct(CreateProductDto productDto)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                ProductName = productDto.ProductName,
                Price = productDto.Price
            };

            repository.CreateProduct(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id}, product.AsDto());
        }
    }

}