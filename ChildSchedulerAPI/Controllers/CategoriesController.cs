using Application.Dto.Categories;
using Application.Interfaces;
using ChildSchedulerAPI.Configuration;
using ChildSchedulerAPI.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildSchedulerAPI.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly SchedulerContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICategoryService _categoryService;

        public CategoriesController(SchedulerContext context,
            ICurrentUserService currentUserService, ICategoryService categoryService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult GetForFamily()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            if (person == null)
                return BadRequest(new { Response = "Brak profilu" });
            var categories = _categoryService.GetCategoriesForFamily(person.FamilyId.GetValueOrDefault());
            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut()]
        public IActionResult Update(UpdateCategoryDto updateCategory)
        {
            _categoryService.UpdateCategory(updateCategory);

            return NoContent();

        }

        // POST: api/Categories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost()]
        public IActionResult Create(CreateCategoryDto newCategory)
        {
            var category = _categoryService.AddNewCategory(newCategory);


            return Created($"api/Category/{category.CategoryId}", category);

        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            return NoContent();

        }
    }
}
