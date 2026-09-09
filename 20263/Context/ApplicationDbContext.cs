using _20263.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _20263.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>(e =>
            {

                e.ToTable("Categories");
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Name).HasMaxLength(60);
                e.Property(x => x.Description).HasMaxLength(250);
                e.Property(x => x.CreatedAtUtc).HasDefaultValueSql("NOW()");
                e.HasIndex(x => x.Name).IsUnique();
            });
        }
        public DbSet<Category> Categories { get; set; }
    }
}