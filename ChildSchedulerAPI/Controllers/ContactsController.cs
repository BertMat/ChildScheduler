using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using System.Net;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Dto.Contacts;
using ChildSchedulerAPI.Repositories;

namespace ChildSchedulerAPI.Controllers
{
    public class ContactsController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly ICurrentUserService _currentUserService;

        public ContactsController(IContactService contactService,
            ICurrentUserService currentUserService)
        {
            _contactService = contactService;
            _currentUserService = currentUserService;
        }

        // GET: api/Contacts
        [HttpGet]
        public IActionResult Get()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            var contacts = _contactService.GetAllContacts(userId);
            return Ok(contacts);
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = _contactService.GetContactById(id);

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
        public IActionResult Update(UpdateContactDto updateContact)
        {
            _contactService.UpdateContact(updateContact);

            return NoContent();

        }

        // POST: api/Contacts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost()]
        public IActionResult Create(CreateContactDto newContact)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });
            newContact.UserId = userId;
            var contact = _contactService.AddNewContact(newContact);


            return Created($"api/Contacts/{contact.Id}", contact);

        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _contactService.DeleteContact(id);

            return NoContent();

        }
    }    
}