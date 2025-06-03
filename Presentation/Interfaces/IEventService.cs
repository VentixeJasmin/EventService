using Data.Entities;
using Presentation.Models;

namespace Presentation.Interfaces; 

public interface IEventService
{
    Task<EventEntity> CreateEvent(EventDto form);
    Task<IEnumerable<EventEntity>> GetAllEvents();
    Task<EventEntity> GetEventById(string id);
    Task<EventEntity> UpdateEvent(string id, EventEntity updatedEvent);
    Task<bool> DeleteEvent(string id);
}
