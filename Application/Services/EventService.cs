using Application.Dto.Events;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public EventDto AddNewEvent(CreateEventDto newEvent)
        {
            var newEventDto = _mapper.Map<Event>(newEvent);
            _eventRepository.Add(newEventDto);

            return _mapper.Map<EventDto>(newEventDto);
        }

        public async Task DeleteEvent(int id)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(id);

            _eventRepository.Delete(existingEvent);
        }

        public IEnumerable<EventDto> GetAllEvents()
        {
            var data = _eventRepository.GetAll();
            return _mapper.Map<IEnumerable<EventDto>>(data);
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var data = await _eventRepository.GetByIdAsync(id);
            return _mapper.Map<EventDto>(data);
        }

        public async Task UpdateEvent(UpdateEventDto updateEvent)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(updateEvent.Id);

            var eventDto = _mapper.Map(updateEvent, existingEvent);
            _eventRepository.Update(eventDto);
        }
    }
}
