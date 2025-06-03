using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Interfaces;
using Data.Interfaces;
using Presentation.Models;
using Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService, ICategoryRepository categoryRepository) : ControllerBase
{
    private readonly IEventService _eventService = eventService;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    [HttpGet("form-data")]
    public async Task<IActionResult> GetEventFormData()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        try
        {
            EventDto dto = new()
            {
                CategoryOptions = await PopulateCategoriesAsync()
            };
            return Ok(dto);
        }
        catch (Exception ex) 
        { 
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(EventDto dto)
    {
        try
        {
            var result = await _eventService.CreateEvent(dto);
            return Created(); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _eventService.GetAllEvents();

        if (events != null)
        {
            //Got help here from ClaudeAI on how to return a list sorted after closest upcoming event)
            var orderedEvents = events
                .Where(e => e.Date >= DateTime.Now)
                .OrderBy(e => e.Date)
                .ToList(); 

            return Ok(orderedEvents);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(string id)
    {
        var entity = await _eventService.GetEventById(id);

        if (entity != null)
        {
            return Ok(entity);
        }
        else
        {
            return NotFound();
        }
    }


    public async Task<List<CategoryEntity>> PopulateCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAsync();
        List<CategoryEntity> options = []; 

        foreach (CategoryEntity category in categories) {
           options.Add(category);
        }

        return options;
    }
}
