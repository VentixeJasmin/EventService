using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Presentation.Interfaces;
using Data.Interfaces;
using Presentation.Models;
using Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService, ICategoryRepository categoryRepository) : ControllerBase
{
    private readonly IEventService _eventService = eventService;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    [Authorize]
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            EventFormDataDto dto = new()
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateEvent(EventDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
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
