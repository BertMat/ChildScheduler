using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        Category Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        IEnumerable<Category> GetForFamily(int id);
    }
}
