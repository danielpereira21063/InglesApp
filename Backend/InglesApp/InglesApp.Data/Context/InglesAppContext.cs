using InglesApp.Domain.Entities;
using InglesApp.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InglesApp.Data.Context
{
    public class InglesAppContext : IdentityDbContext<User, Role, int>
    {
        public InglesAppContext() { }
        public InglesAppContext(DbContextOptions<InglesAppContext> options) : base(options) { }

        public DbSet<Vocabulario> Vocabularios { get; set; }
        public DbSet<Pratica> Praticas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vocabulario>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<Pratica>()
                .HasKey(p => p.Id);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InglesAppContext).Assembly);
        }
    }
}
