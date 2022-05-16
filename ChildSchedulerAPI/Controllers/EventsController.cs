using Application.Dto.Events;
using Application.Dto.Responses;
using Application.Interfaces;
using ChildSchedulerAPI.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChildSchedulerAPI.Controllers
{
    public class EventsController : BaseController
    {
        private readonly SchedulerContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEventService _eventService;

        public EventsController(SchedulerContext context,
            ICurrentUserService currentUserService, IEventService eventService,
            IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _currentUserService = currentUserService;
            _eventService = eventService;
        }

        // GET: api/Events
        [HttpGet]
        public IActionResult Get()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            if (person == null)
                return BadRequest(new { Response = "Brak profilu" });

            var events = _eventService.GetAllEvents().Where(p => p.FamilyId == person.FamilyId).ToList();
            return Ok(events);
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            if (person == null)
                return BadRequest(new { Response = "Brak profilu" });

            var eventDto = await _eventService.GetEventByIdAsync(id);

            if (eventDto == null || eventDto.FamilyId != person.FamilyId)
            {
                return NotFound();
            }

            return Ok(eventDto);
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut()]
        public async Task<IActionResult> Update(UpdateEventDto updateEvent)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);
            var categoryId = _context.Categories.FirstOrDefault(p => p.FamilyId == person.FamilyId && p.CategoryName == updateEvent.CategoryName).CategoryId;

            var children = _context.Children.Where(p => updateEvent.Children.Select(x => x.Id).Contains(p.Id)).ToList();


            var people = _context.People.Where(p => updateEvent.People.Select(x => x.PersonId).Contains(p.PersonId)).ToList();

            var contacts = _context.Contacts.Where(p => updateEvent.Contacts.Select(x => x.Id).Contains(p.Id)).ToList();
            if (people.Any())
                updateEvent.People = people;
            if (contacts.Any())
                updateEvent.Contacts = contacts;
            if (children.Any())
                updateEvent.Children = children;

            updateEvent.FamilyId = person.FamilyId.GetValueOrDefault();
            updateEvent.EventTypeId = 1;
            updateEvent.CategoryId = categoryId;

            await _eventService.UpdateEvent(updateEvent);

            return NoContent();

        }

        // POST: api/Events
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost()]
        public IActionResult Create(CreateEventDto newEvent)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);
            var categoryId = _context.Categories.FirstOrDefault(p => p.FamilyId == person.FamilyId && p.CategoryName == newEvent.CategoryName).CategoryId;
            
            var children = _context.Children.Where(p => newEvent.Children.Select(x => x.Id).Contains(p.Id)).ToList();


            var people = _context.People.Where(p => newEvent.People.Select(x => x.PersonId).Contains(p.PersonId)).ToList();

            var contacts = _context.Contacts.Where(p => newEvent.Contacts.Select(x => x.Id).Contains(p.Id)).ToList();
            if (people.Any())
                newEvent.People = people;
            if (contacts.Any())
                newEvent.Contacts = contacts;
            if (children.Any())
                newEvent.Children = children;

            newEvent.FamilyId = person.FamilyId.GetValueOrDefault();
            newEvent.EventTypeId = 1;
            newEvent.CategoryId = categoryId;
            var eventDto = _eventService.AddNewEvent(newEvent);


            return Created($"api/Events/{eventDto.Id}", eventDto);

        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventService.DeleteEvent(id);

            return NoContent();

        }

        // GET: api/Events
        [HttpGet("Photos/{eventId}")]
        public IActionResult GetPhotos(int eventId)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            if (person == null)
                return BadRequest(new { Response = "Brak profilu" });

            var photos = _context.EventPhotos.Where(p => p.EventId == eventId);
            return Ok(photos);
        }
        [HttpPost("UploadFile/{eventId}")]
        public async Task<IActionResult> GetFile(int eventId)
        {
            try
            {
                var httpRequest = HttpContext.Request.Form;
                var _event = _context.Events.Where(p => p.Id == eventId).FirstOrDefault();
                if (httpRequest.Files.Count > 0)
                {
                    foreach (IFormFile file in httpRequest.Files)
                    {
                        var postedFile = file;


                        string contentRootPath = _webHostEnvironment.ContentRootPath;
                        var mainFilePath = Path.Combine(contentRootPath, "uploads");
                        var filePath = Path.Combine(contentRootPath, "uploads", $"{_event.FamilyId}");

                        if (!Directory.Exists(mainFilePath))
                            Directory.CreateDirectory(mainFilePath);
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            var bytes = memoryStream.ToArray();
                            var eventPhoto = new EventPhoto
                            {
                                EventId = _event.Id,
                                Photo = bytes,
                                EventPhotoDescription = "TEST"
                            };
                            _context.EventPhotos.Add(eventPhoto);
                            _context.SaveChanges();

                            var fileName = "EventPhoto" + eventPhoto.EventPhotoId + "_" + postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                            System.IO.File.WriteAllBytes(Path.Combine(filePath, fileName), bytes);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                return NoContent();
            }

            return NoContent();
        }
    }
}
