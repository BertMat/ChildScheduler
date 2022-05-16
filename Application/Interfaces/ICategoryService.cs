using Application.Dto.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        CategoryDto GetCategoryById(int id);
        CategoryDto AddNewCategory(CreateCategoryDto category);
        void UpdateCategory(UpdateCategoryDto category);
        void DeleteCategory(int id);
        IEnumerable<CategoryDto> GetCategoriesForFamily(int id);
    }
}
