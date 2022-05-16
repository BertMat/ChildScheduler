using Application.Dto.Categories;
using Application.Dto.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEventService
    {
        IEnumerable<EventDto> GetAllEvents();
        Task<EventDto> GetEventByIdAsync(int id);
        EventDto AddNewEvent(CreateEventDto newEvent);
        Task UpdateEvent(UpdateEventDto updateEvent);
        Task DeleteEvent(int id);
    }
}
