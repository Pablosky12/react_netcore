using angular_netcore.Models;
using Microsoft.EntityFrameworkCore;

namespace angular_netcore.Persistance
{
    public class VegaDbContext: DbContext
    {
        public VegaDbContext(DbContextOptions<VegaDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Make>().Property(b => b.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Model>().Property(b => b.Id).ValueGeneratedOnAdd();

        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }

    }
}