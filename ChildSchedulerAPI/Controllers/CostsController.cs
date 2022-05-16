using ChildSchedulerAPI.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Application.Interfaces;
using Application.Dto.Costs;
using System.Threading.Tasks;

namespace ChildSchedulerAPI.Controllers
{
    public class CostsController : BaseController
    {
        private readonly SchedulerContext _context;
        private readonly ICostService _costService;
        private readonly ICurrentUserService _currentUserService;

        public CostsController(SchedulerContext context,
            ICostService costService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _costService = costService;
            _currentUserService = currentUserService;
        }

        // GET: api/Costs
        [HttpGet]
        public IActionResult Get()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            if (person == null)
                return BadRequest(new { Response = "Brak profilu" });

            var costs = _costService.GetAllCosts(person.FamilyId.GetValueOrDefault());
            return Ok(costs);
        }
        // GET: api/Costs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            if (person == null)
                return BadRequest(new { Response = "Brak profilu" });

            var costDto = await _costService.GetCostById(id);

            if (costDto == null)
            {
                return NotFound();
            }

            return Ok(costDto);
        }

        // GET: api/Costs
        [HttpGet]
        [Route("New")]
        public IActionResult GetNew()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            if (person == null)
                return BadRequest(new { Response = "Brak profilu" });

            var cost = new Cost
            {
                CategoryId = 1,
                CostDate = DateTime.Today,
                CostName = "Podatki",
                CostDescription = "Podatki Opis",
                Value = 2000
            };
            _context.Costs.Add(cost);
            _context.SaveChanges();


            //var costs = _costService.GetAllCosts();
            return Ok();
        }
        // GET: api/Costs
        [HttpPost]
        public IActionResult Create(CreateCostDto newCost)
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { Response = "Brak użytkownika" });

            var person = _context.People.FirstOrDefault(p => p.UserId == userId);

            if (person == null)
                return BadRequest(new { Response = "Brak profilu" });


            var costDto = _costService.AddNewCost(newCost);

            return Created($"api/Costs/{costDto.CostId}", costDto);
        }
    }
}
