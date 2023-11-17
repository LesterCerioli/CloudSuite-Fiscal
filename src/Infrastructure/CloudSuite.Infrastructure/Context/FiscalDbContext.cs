using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Reflection.Emit;
using CloudSuite.Infrastructure.Mapping.EFCore;
using CloudSuite.Modules.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
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

            modelBuilder.Entity<Address>(c =>
            {
                c.ToTable("Addresses");
            });

            modelBuilder.Entity<CancelOrder>(c =>
            {
                c.ToTable("CancelOrders");
            });

            modelBuilder.Entity<City>(c =>
            {
                c.ToTable("Cities");
            });

            modelBuilder.Entity<Country>(c =>
            {
                c.ToTable("Countries");
            });

            modelBuilder.Entity<Darf>(c =>
            {
                c.ToTable("Darfs");
            });

            modelBuilder.Entity<DAS>(c =>
            {
                c.ToTable("DASes");
            });

            modelBuilder.Entity<DeclaracaoIR>(c =>
            {
                c.ToTable("DeclaracaoIRs");
            });

            modelBuilder.Entity<District>(c => 
            {
                c.ToTable("Districts");
            });

            modelBuilder.Entity<FederalTax>(c =>
            {
                c.ToTable("FederalTaxes");
            });

            modelBuilder.Entity<IdeCancelamento>(c =>
            {
                c.ToTable("IdeCancelamentos");
            });

            modelBuilder.Entity<IdeNFSe>(a =>
            {
                c.ToTable("IdeNFSes");
            });

            modelBuilder.Entity<Note>(c =>
            {
                c.ToTable("Notes");
            });


            
            
            base.OnModelCreating(modelBuilder);
        }

    }
}