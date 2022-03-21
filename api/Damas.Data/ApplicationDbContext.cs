using Damas.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Damas.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<Piece> Pieces => Set<Piece>();
        public DbSet<Move> Moves => Set<Move>();

        private IConfiguration? _config;

        public ApplicationDbContext(IConfiguration config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Configure();
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await Database.BeginTransactionAsync();
        }

        new public async Task SaveChanges()
        {
            await SaveChangesAsync();
        }

        public async Task Migrate()
        {
            await Database.MigrateAsync();
        }
    }
}