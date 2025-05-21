using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models;

public class TicketTypeDto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string EventId { get; set; } = null!;

    [Required]
    public string Title { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public string Description { get; set; } = null!;

}
