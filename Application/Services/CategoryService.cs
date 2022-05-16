using Application.Dto.Categories;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public CategoryDto AddNewCategory(CreateCategoryDto category)
        {
            var newCategory = _mapper.Map<Category>(category);
            _categoryRepository.Add(newCategory);

            return _mapper.Map<CategoryDto>(newCategory);
        }


        public void DeleteCategory(int id)
        {

            var existingCategory = _categoryRepository.GetById(id);

            _categoryRepository.Delete(existingCategory);
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var data = _categoryRepository.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(data);
        }

        public CategoryDto GetCategoryById(int id)
        {
            var data = _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDto>(data);
        }
        public IEnumerable<CategoryDto> GetCategoriesForFamily(int id)
        {
            var data = _categoryRepository.GetForFamily(id);
            return _mapper.Map<IEnumerable<CategoryDto>>(data);
        }

        public void UpdateCategory(UpdateCategoryDto updateCategory)
        {
            var existingCategory = _categoryRepository.GetById(updateCategory.FamilyId);

            var category = _mapper.Map(updateCategory, existingCategory);
            _categoryRepository.Update(category);

        }
    }
}
