using Application.Dto.People;
using Application.Interfaces;
using ChildSchedulerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChildSchedulerAPI.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ICurrentUserService _currentUserService;

        public PersonController(IPersonService personService, ICurrentUserService currentUserService)
        {
            _personService = personService;
            _currentUserService = currentUserService;
        }

        // GET: api/Contacts
        [HttpGet]
        public IActionResult Get()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            var contact = _personService.GetPersonByUser(userId);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
        /// <summary>
        /// Taki przykład O
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Przykładowy format JSON:
        ///
        ///     {
        ///        "ContractorId": 1,
        ///        "AddressId": 4
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut("Test")]
        public IActionResult Get(object model)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            var contact = _personService.GetPersonByUser(userId);
            var json = JsonConvert.SerializeObject(model);
            var user = JsonConvert.DeserializeObject<PersonDto>(json);

            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(PersonDto));

            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prp = props[i];
                if (json.Contains(prp.Name))
                {
                    var propInfo = contact.GetType().GetProperty(prp.Name);
                    propInfo.SetValue(contact, user.GetType().GetProperty(prp.Name).GetValue(user));
                }
            }
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            var contact = _personService.GetPersonByUser(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut()]
        public IActionResult Update(UpdatePersonDto updatePerson)
        {
            _personService.UpdatePerson(updatePerson);

            return NoContent();

        }

        // POST: api/Contacts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost()]
        public IActionResult Create(CreatePersonDto newPerson)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            newPerson.UserId = userId;
            var person = _personService.AddNewPerson(newPerson);


            return Created($"api/Person/{person.PersonId}", person);

        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personService.DeletePerson(id);

            return NoContent();

        }
        [HttpPost("LeaveFamily/{userId?}")]
        public async Task<object> LeaveFamily(string? userId = null)
        {
            // Check if user is a family owner
            //var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            PersonDto person = _personService.GetPersonByUser(userId);

            return Ok();
        }
    }
}
