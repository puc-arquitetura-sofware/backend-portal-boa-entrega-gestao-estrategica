using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GSL.GestaoEstrategica.Data.Repositories
{
    public class GestaoEstrategicaDbContext: DbContext
    {
        public DbContextOptions<GestaoEstrategicaDbContext> _options;

        public GestaoEstrategicaDbContext(DbContextOptions<GestaoEstrategicaDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoEstrategicaDbContext).Assembly);
        }

    }
}