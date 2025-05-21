using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class EventRepository(DataContext context) : BaseRepository<EventEntity>(context), IEventRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<EventEntity>> GetAsync()
    {
        return await _context.Set<EventEntity>()
            .Include(e => e.Category)
            .ToListAsync();
    }

    public override async Task<EventEntity> GetAsync(Expression<Func<EventEntity, bool>> expression)
    {
        var entity = await _context.Set<EventEntity>()
        .Include(e => e.Category)
        .FirstOrDefaultAsync(expression); 

        return entity ?? new EventEntity();
    }
}

