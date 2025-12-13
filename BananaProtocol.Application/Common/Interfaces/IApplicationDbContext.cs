using BananaProtocol.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BananaProtocol.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}