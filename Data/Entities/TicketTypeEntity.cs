using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class TicketTypeEntity
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string EventId { get; set; } = null!;

    [System.Text.Json.Serialization.JsonIgnore]
    public EventEntity Event { get; set; } = null!;

    [Required]
    public string Title { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; } 

    [Required]
    public string Description { get; set; } = null!;
}
