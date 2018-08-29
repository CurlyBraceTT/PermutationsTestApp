using Microsoft.EntityFrameworkCore;

namespace PermutationsTestApp.Model
{
    public class PermutationsContext : DbContext
    {
        public DbSet<Element> Elements { get; set; }

        public PermutationsContext() : base()
        { }

        public PermutationsContext(DbContextOptions<PermutationsContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Element>()
                .HasIndex(p => p.Value)
                .IsUnique();
        }
    }
}
