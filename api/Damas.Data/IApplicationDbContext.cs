using Damas.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Damas.Data
{
    public interface IApplicationDbContext : IAsyncDisposable
    {
        DbSet<Player> Players { get; }
        DbSet<Match> Matches { get; }
        DbSet<Piece> Pieces { get; }
        DbSet<Move> Moves { get; }

        Task SaveChanges();
        Task<IDbContextTransaction> BeginTransaction();
        Task Migrate();
    }
}

