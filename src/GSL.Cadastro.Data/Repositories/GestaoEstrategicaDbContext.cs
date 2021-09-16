using GSL.GestaoEstrategica.Dominio.Models.Entidades;
using GSL.GestaoEstrategica.SharedKernel.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace GSL.GestaoEstrategica.Data.Repositories
{
    public class GestaoEstrategicaDbContext: DbContext
    {
        //private IDbContextTransaction _transaction;
        public DbContextOptions<GestaoEstrategicaDbContext> _options;

        public GestaoEstrategicaDbContext(DbContextOptions<GestaoEstrategicaDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Entrega> Entregas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Documento>();
            modelBuilder.Ignore<Cpf>();
            modelBuilder.Ignore<Cnpj>();
            modelBuilder.Ignore<Email>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoEstrategicaDbContext).Assembly);
        }

    }
}