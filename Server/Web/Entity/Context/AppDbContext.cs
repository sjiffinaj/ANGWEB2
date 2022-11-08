using Microsoft.EntityFrameworkCore;

namespace Web.Entity.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Entity<User>().ToTable("users");
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

    }
}
