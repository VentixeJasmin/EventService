using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Presentation.Interfaces;
using Presentation.Models;

namespace Presentation.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<EventEntity> CreateEvent(EventDto form)
    {
        try
        {
            if (form == null)
                return null!;

            //if (form.EventImage != null && form.EventImage.Length > 0)
            //{
            //    var imageFileUri = await _fileHandler.UploadFileAsync(form.EventImage);
            //    string eventImagePath = imageFileUri;

            //}

            EventEntity entity = new()
            {
                Title = form.Title,
                Date = form.Date,
                Description = form.Description ?? null!,
                //TicketTypes = form.SelectedTicketTypes, 
                CategoryId = form.CategoryId,
                VenueId = form.VenueId
            };

            var newEvent = await _eventRepository.CreateAsync(entity);

            return newEvent; 
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
            return null!; 
        }
    }

    public async Task<IEnumerable<EventEntity>> GetAllEvents()
    {
        try
        {
            return await _eventRepository.GetAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null!;
        }
    }

    public async Task<EventEntity> GetEventById(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id)) 
                return null!;   

            return await _eventRepository.GetAsync(e => e.Id == id);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null!;
        }
    }

    public async Task<EventEntity> UpdateEvent(string id, EventEntity updatedEvent)
    {
        try
        {
            if (string.IsNullOrEmpty(id))
                return null!;

            return await _eventRepository.UpdateAsync(e => e.Id == id, updatedEvent); 

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null!;
        }
    }

    public async Task<bool> DeleteEvent(string id)
    {
        try {
            if (string.IsNullOrEmpty(id))
                return false;

            return await _eventRepository.DeleteAsync(e => e.Id == id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }
}
