using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CategoryEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!; 

    public string? Icon { get; set; }
}
