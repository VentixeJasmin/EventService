using Data.Entities;

namespace Presentation.Models;

public class EventFormDataDto
{
    public List<CategoryEntity> CategoryOptions { get; set; } = new();
}
