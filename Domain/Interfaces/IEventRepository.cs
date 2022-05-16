using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
        Task<Event> GetByIdAsync(int id);
        Event Add(Event newEvent);
        void Update(Event updateEvent);
        void Delete(Event deleteEvent);
    }
}
