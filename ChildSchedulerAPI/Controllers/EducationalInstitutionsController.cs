using Application.Dto.Children;
using Application.Dto.EducationalInstitutions;
using Application.Dto.Families;
using Application.Dto.Responses;
using Application.Interfaces;
using ChildSchedulerAPI.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildSchedulerAPI.Controllers
{
    public class EducationalInstitutionsController : BaseController
    {
        private readonly SchedulerContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPersonService _personService;
        private readonly IChildService _childService;
        private readonly IFamilyService _familyService;

        public EducationalInstitutionsController(SchedulerContext context,
            ICurrentUserService currentUserService, IPersonService personService,
            IChildService childService,
            IFamilyService familyService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _personService = personService;
            _childService = childService;
            _familyService = familyService;
        }

        [HttpPost()]
        public IActionResult CreateEducInstitution(CreateEducationalInstitutionDto newEducInstitution)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.SingleOrDefault(p => p.UserId == userId);
            if (person == null)
                return NotFound();

            var institution = _familyService.AddNewInstitution(newEducInstitution);

            _context.SaveChanges();


            return Ok(new { Response = $"Utworzono placówkę edukacyjną został dodany do rodziny" });

        }
        [HttpPut()]
        public IActionResult UpdateEducInstitution(UpdateEducationalInstitutionDto newEducInstitution)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.SingleOrDefault(p => p.UserId == userId);
            if (person == null)
                return NotFound();

            _familyService.UpdateInstitution(newEducInstitution);

            return Ok(new { Response = $"Zaktualizowano placówkę edukacyjną" });

        }
        // GET: api/Families
        [HttpGet]
        public IActionResult GetEducationalInstitution()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            // Get the family to which the user belongs
            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            var institutions = _familyService.GetAllInstitutions(person.FamilyId.GetValueOrDefault());

            return institutions != null ? Ok(institutions) : BadRequest();
        }
        // GET: api/Families
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEducationalInstitution(int id)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });


            var institution = _familyService.GetInsitutionById(id);

            return institution != null ? Ok(institution) : BadRequest();
        }
    }
}
