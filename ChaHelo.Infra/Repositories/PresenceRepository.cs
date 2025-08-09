using System;
using ChaHelo.Domain.Entities;
using ChaHelo.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ChaHelo.Infra.Repositories;

public class PresenceRepository
{
    private readonly ChaHeloDbContext _context;

    public PresenceRepository(ChaHeloDbContext context)
    {
        _context = context;
    }

    public DbSet<Presence> Presences => _context.Presences;

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
}
