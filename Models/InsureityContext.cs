using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace InsureityAPI.Models
{
    public class InsureityContext:DbContext
    {
        public InsureityContext(DbContextOptions<InsureityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoginDetails>(entity =>
            {
                entity.HasIndex(u => u.UserEmail).IsUnique();
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.HasIndex(b => b.BusinessName).IsUnique();
            });

            modelBuilder.Entity<Consumer>(entity =>
            {
                entity.HasIndex(c => c.ConsumerEmail).IsUnique();
                entity.HasIndex(c => c.PAN).IsUnique();
            });
            modelBuilder.Entity<Quote>()
                .HasMany(e => e.Policies)
                .WithOne()
                .HasForeignKey(e=>e.QuoteId);

            modelBuilder.Entity<Business>()
                .HasMany(e => e.Properties)
                .WithOne(e => e.Business)
                .HasForeignKey(e => e.BusinessID)
                .HasPrincipalKey(e => e.BusinessId);

            modelBuilder.Entity<Quote>()
                .HasOne(e => e.Business);

            modelBuilder.Entity<LoginDetails>()
                .HasMany(e => e.Policies)
                .WithOne()
                .HasForeignKey(e => e.AgentId);
          

            modelBuilder.Entity<Policy>()
                .HasOne(e => e.Property);
            modelBuilder.Entity<Property>()
                .HasOne(e => e.Business);
            modelBuilder.Entity<Business>()
                .HasOne(e => e.Consumer);
            modelBuilder.Entity<Consumer>()
                .HasOne(e => e.Agent);
        }
        public DbSet<LoginDetails> LoginDetailsList { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<PolicyMaster> PoliciesMaster { get; set; }
        public DbSet<BusinessMaster> BusinessMaster { get; set; }
        public DbSet<PropertyMaster> PropertyMaster { get; set; }
    }
}
