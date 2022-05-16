using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly SchedulerContext _schedulerContext;

        public EventRepository(SchedulerContext schedulerContext)
        {
            _schedulerContext = schedulerContext;
        }

        public Event Add(Event newEvent)
        {
            _schedulerContext.Events.Add(newEvent);
            try
            {
                _schedulerContext.SaveChanges();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return newEvent;
        }

        public void Delete(Event deleteEvent)
        {
            _schedulerContext.Remove(deleteEvent);
            _schedulerContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _schedulerContext.Events.Include(p => p.Category).Include(p => p.Family);
                //.Include(p => p.Costs)
                //.Include(p => p.Children)
                //.Include(p => p.People)
                //.Include(p => p.Contacts)
                //.Include(p => p.Photos)
                //.Include(p => p.EducationalInstitution);
            return events;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _schedulerContext.Events.Include(p => p.Category).Include(p => p.Family)
                .Include(p => p.Costs)
                .Include(p => p.Children)
                .Include(p => p.Contacts)
                .Include(p => p.People)
                .Include(p => p.EducationalInstitution).SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Event updateEvent)
        {
            _schedulerContext.Events.Update(updateEvent);
            _schedulerContext.SaveChanges();
        }
    }
}
