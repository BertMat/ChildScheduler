using Application.Dto.Children;
using Application.Dto.EducationalInstitutions;
using Application.Dto.Families;
using Application.Dto.Responses;
using Application.Interfaces;
using ChildSchedulerAPI.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChildSchedulerAPI.Controllers
{
    public class FamiliesController : BaseController
    {
        private readonly SchedulerContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPersonService _personService;
        private readonly IChildService _childService;
        private readonly IFamilyService _familyService;

        public FamiliesController(SchedulerContext context,
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

        // GET: api/Families
        [HttpGet]
        public IActionResult Get()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            // Get the family to which the user belongs
            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            var family = _familyService.GetFamilyById(person.FamilyId.GetValueOrDefault());
            
            return family != null ? Ok(family) : BadRequest();
        }
        // GET: api/Families
        [HttpGet("members")]
        public IActionResult GetFamilyMembers()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            if (person == null)
                return BadRequest(new { Response = "Brak profilu" });

            // Get the family to which the user belongs
            var familyMembers = _personService.GetFamilyMembers(person.PersonId);
            return familyMembers != null ? Ok(familyMembers) : NotFound();
        }


        // PUT: api/Families/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut()]
        public IActionResult Update(UpdateFamilyDto updateFamily)
        {
            _familyService.UpdateFamily(updateFamily);

            return NoContent();

        }

        // POST: api/Families
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost()]
        public IActionResult Create(CreateFamilyDto newFamily)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            newFamily.UserId = userId;

            var person = _context.People.SingleOrDefault(p => p.UserId == userId);
            if (person == null)
                return NotFound();
            if (person.FamilyId.HasValue)
            {
                return BadRequest(new{ Response = "Użytkownik należy już do rodziny." });
            }
            
            var family = _familyService.AddNewFamily(newFamily);
            
            person.FamilyId = family.FamilyId;
            _context.SaveChanges();


            return Created($"api/Families/{family.UserId}", family);

        }

        // DELETE: api/Families/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _familyService.DeleteFamily(id);

            return NoContent();

        }
        [HttpDelete("Leave")]
        public async Task<IActionResult> LeaveFamily()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.SingleOrDefault(p => p.UserId == userId);
            if (person == null)
                return NotFound();

            if (!person.FamilyId.HasValue)
            {
                return BadRequest("Użytkownik nie należy do żadnej rodziny.");
            }
            var family = _context.Families.SingleOrDefault(p => p.FamilyId == person.FamilyId);

            if(family.UserId == family.UserId)
            {
                return BadRequest("Głowa rodziny nie może odejść od rodziny.");
            }

            person.FamilyId = null;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("Owner/{personId}")]
        public async Task<IActionResult> ChangeFamilyOwnerAsync(int personId)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var family = _context.Families.SingleOrDefault(p => p.UserId == userId);

            var person = _context.People.SingleOrDefault(p => p.PersonId == personId);

            if (person == null)
                return NotFound();
            family.UserId = person.UserId;
            _context.Update(family);
            await _context.SaveChangesAsync();

            return Ok();

        }
        [HttpGet("Delete/{personId}")]
        public async Task<IActionResult> DeletePersonFromFamily(int personId)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.SingleOrDefault(p => p.PersonId == personId);

            if (person == null)
                return NotFound();
            person.FamilyId = null;
            _context.Update(person);
            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpGet()]
        [Route("DbTest")]
        [AllowAnonymous]
        public IActionResult DbTest(int requestNumber)
        {
            for(int i = 0; i < requestNumber; i++)
            {
                var family = _context.Families.Where(p => p.FamilyId == 100).FirstOrDefault();
            }
            return NoContent();
        }
        [HttpGet()]
        [Route("Join")]
        [AllowAnonymous]
        public IActionResult JoinToFamily(string key)
        {
            var existingInvite = _context.FamilyInvites.FirstOrDefault(p => p.InviteKey == key);

            if (existingInvite == null)
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Brak zaproszenia z takim kluczem"
                    },
                    Success = false
                });

            var existingFamily = _familyService.GetFamilyById(existingInvite.FamilyId);

            if (existingFamily == null)
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                        "Brak takiej rodziny"
                    },
                    Success = false
                });

            _familyService.AddPersonToFamily(existingFamily.FamilyId, existingInvite.PersonId);

            return Ok($"Użytkownik został dodany do rodziny: {existingFamily.FamilyName}");
        }

    }
}
