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
using Application.Dto.SocialMedias;

namespace ChildSchedulerAPI.Controllers
{
    public class SocialMediasController : BaseController
    {
        private readonly ISocialMediaService _socialMediaService;

        public SocialMediasController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        // GET: api/SocialMedias
        [HttpGet]
        public IActionResult Get()
        {
            var SocialMedias = _socialMediaService.GetAllSocialMedias();
            return Ok(SocialMedias);
        }

        // GET: api/SocialMedias/contact/5
        [HttpGet("contact/{id}")]
        public IActionResult GetForContact(int id)
        {
            var SocialMedias = _socialMediaService.GetAllSocialMediasForContact(id);
            return Ok(SocialMedias);
        }

        // GET: api/SocialMedias/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = _socialMediaService.GetSocialMediaById(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/SocialMedias/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut()]
        public IActionResult Update(UpdateSocialMediaDto updateContact)
        {
            _socialMediaService.UpdateSocialMedia(updateContact);

            return NoContent();

        }

        // POST: api/SocialMedias
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost()]
        public IActionResult Create(CreateSocialMediaDto newSocialMediaDto)
        {
            var socialMedia = _socialMediaService.AddNewSocialMedia(newSocialMediaDto);


            return Created($"api/SocialMedias/{socialMedia.Id}", socialMedia);

        }

        // DELETE: api/SocialMedias/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _socialMediaService.DeleteSocialMedia(id);

            return NoContent();

        }
    }    
}