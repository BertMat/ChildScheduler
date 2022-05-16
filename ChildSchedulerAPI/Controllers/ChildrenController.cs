using Application.Dto.ChildHistories;
using Application.Dto.Children;
using Application.Interfaces;
using ChildSchedulerAPI.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildSchedulerAPI.Controllers
{
    public class ChildrenController : BaseController
    {

        private readonly SchedulerContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPersonService _personService;
        private readonly IChildService _childService;
        private readonly IChildHistoryService _childHistoryService;
        private readonly IFamilyService _familyService;

        public ChildrenController(SchedulerContext context,
            ICurrentUserService currentUserService, IPersonService personService,
            IChildService childService,
            IChildHistoryService childHistoryService,
            IFamilyService familyService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _personService = personService;
            _childService = childService;
            _childHistoryService = childHistoryService;
            _familyService = familyService;
        }

        [HttpGet()]
        public IActionResult GetChildren()
        {

            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            var person = _context.People.SingleOrDefault(p => p.UserId == userId);
            if (person == null)
                return NotFound();

            var children = _childService.GetAllChildrenForFamily(person.FamilyId.GetValueOrDefault());

            return children != null ? Ok(children) : NotFound();
        }
        [HttpPost()]
        public IActionResult AddChild(CreateChildDto createChildDto)
        {

            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            var person = _context.People.SingleOrDefault(p => p.UserId == userId);
            if (person == null)
                return NotFound();

            createChildDto.FamilyId = person.FamilyId.GetValueOrDefault();
            var child = _childService.AddNewChild(createChildDto);

            var childHistory = new CreateChildHistoryDto
            {
                Height = child.Height,
                Weight = child.Weight,
                ChildId = child.Id,
            };
            var newChildHistory = _childHistoryService.AddNewChildHistory(childHistory);
            return Ok(child);
        }
        [HttpGet()]
        [Route("ChildHistory/{childId}")]
        public async Task<IActionResult> GetChildHistory(int childId)
        {
            var childHistory = _context.ChildHistories;

            return Ok(childHistory);

        }
        [HttpGet()]
        [Route("{id}")]
        public async Task<IActionResult> GetChildById(int id)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            var person = _context.People.SingleOrDefault(p => p.UserId == userId);
            if (person == null)
                return NotFound();

            var child = await _childService.GetChildById(id);

            return Ok(child);

        }
        [HttpPut()]
        public async Task<IActionResult> UpdateChild(UpdateChildDto updateChild)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.SingleOrDefault(p => p.UserId == userId);
            if (person == null)
                return NotFound();

            var existingChild = await _childService.GetChildById(updateChild.Id);


            await _childService.UpdateChild(updateChild);
            if(existingChild.Height != updateChild.Height || existingChild.Weight != updateChild.Weight)
            {
                var childHistory = new CreateChildHistoryDto
                {
                    Height = updateChild.Height,
                    Weight = updateChild.Weight,
                    ChildId = updateChild.Id,
                };

                var newChildHistory = _childHistoryService.AddNewChildHistory(childHistory);
            }

            return Ok(new { Response = $"Zaktualizowano profil dziecka" });

        }
    }
}
