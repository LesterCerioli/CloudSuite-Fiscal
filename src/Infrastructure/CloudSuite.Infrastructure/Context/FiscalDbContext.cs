using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using CloudSuite.Infrastructure.Mapping.EFCore;
using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Messaging;

namespace CloudSuite.Infrastructure.Context
{
    public class FiscalDbContext : DbContext
    {
        public FiscalDbContext(DbContextOptions<FiscalDbContext> options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Address> Addresses {get; set;}

        public DbSet<CancelOrder> CancelOrders {get; set;}

        public DbSet<City> Cities {get; set;}

        public DbSet<Country> Countries {get; set;}

        public DbSet<Darf> Darfs {get; set;}

        public DbSet<DAS> DASes {get; set;}

        public DbSet<DeclaracaoIR> DeclaracaoIRs {get; set;}

        public DbSet<District> Districts {get; set;}

        public DbSet<FederalTax> FederalTaxes {get; set;}

        public DbSet<IdeCancelamento> IdeCancelamentos {get; set;}

        public DbSet<IdeNFSe> IdeNFSes {get; set;}

        public DbSet<Note> Notes {get; set;}

        public DbSet<Prestador> Prestadores {get; set;}

        public DbSet<State> States {get; set;}

        public DbSet<TomadorServico> TomadorServicos {get; set;}

        protected override void OodelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Ignore<Event>();
            
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                    e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfiguration(new AddressEFCoreMapping();
            
            modelBuilder.ApplyConfiguration(new CancelOrderEFCoremapping());

            modelBuilder.ApplyConfiguration(new CityEFCoreMapping());

            modelBuilder.ApplyConfiguration(new CountryEFCoreMapping());

            modelBuilder.ApplyConfiguration(new DarfEFCoreMapping());

            modelBuilder.ApplyConfiguration(new DASEFCoremapping());

            modelBuilder.ApplyConfiguration(new DeclaracaoIREFCoreMapping());

            modelBuilder.ApplyConfiguration(new DistrictEFCoreMapping());

            modelBuilder.ApplyConfiguration(new FederalTaxMapping());

            modelBuilder.ApplyConfiguration(new IdeCancelamentoEFCoreMapping());

            modelBuilder.ApplyConfiguration(new IdeNFSeEFCoremapping());

            modelBuilder.ApplyConfiguration(new NoteEFCoreMapping());

            
            
            base.OnModelCreating(modelBuilder);
        }

    }
}