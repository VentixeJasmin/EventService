using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Models; 

public class EventDto
{
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; }

    public string? Description { get; set; }

    public IFormFile? EventImage { get; set; } 
    
    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int VenueId { get; set; }
}
