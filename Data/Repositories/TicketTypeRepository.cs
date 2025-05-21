using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class TicketTypeRepository(DataContext context) : BaseRepository<TicketTypeEntity>(context), ITicketTypeRepository
{
    private readonly DataContext _context = context;
}

