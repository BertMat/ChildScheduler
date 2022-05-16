using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SchedulerContext _schedulerContext;

        public CategoryRepository(SchedulerContext schedulerContext)
        {
            _schedulerContext = schedulerContext;
        }

        public Category Add(Category category)
        {
            _schedulerContext.Categories.Add(category);
            _schedulerContext.SaveChanges();
            return category;
        }

        public void Delete(Category category)
        {
            _schedulerContext.Remove(category);
            _schedulerContext.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return _schedulerContext.Categories;
        }

        public Category GetById(int id)
        {
            return _schedulerContext.Categories.SingleOrDefault(p => p.CategoryId == id);
        }
        public IEnumerable<Category> GetForFamily(int id)
        {
            return _schedulerContext.Categories.Where(p => p.FamilyId == id);
        }

        public void Update(Category category)
        {
            _schedulerContext.Categories.Update(category);
            _schedulerContext.SaveChanges();
        }
    }
}
