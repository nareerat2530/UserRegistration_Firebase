﻿using UserRegistration_Tutorial.DTO.Events;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventReadDto>> GetAllEventsAsync();
    Task AddNewEvent();
    Task DeleteEventsAsync(string id);
    Task UpdateEventsAsync(Events events);
    Task <DocumentReference> GetEventsByIdAsync(int id);
}