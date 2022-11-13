using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

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
            //modelBuilder.HasDefaultSchema("public");
            //modelBuilder.Entity<User>().ToTable("users");

            UserRelations(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void UserRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.UserType)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserTypeId);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var applicationUserId = 1;
            var applicationDate = DateTime.Now;
            UpdateBaseEntity(applicationUserId, applicationDate);
            return base.SaveChangesAsync(cancellationToken);
        }


        public override int SaveChanges()
        {
            var applicationUserId = 1;
            var applicationDate = DateTime.Now;
            UpdateBaseEntity(applicationUserId, applicationDate);
            return base.SaveChanges();
        }

        private void UpdateBaseEntity(int applicationUserId, DateTime applicationDate)
        {
            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
            addedEntities.ForEach(x =>
            {
                x.Property("CreatedDate").CurrentValue = applicationDate;
                x.Property("CreatedBy").CurrentValue = applicationUserId;
                x.Property("ModifiedDate").CurrentValue = applicationDate;
                x.Property("ModifiedBy").CurrentValue = applicationUserId;
            });

            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
            modifiedEntities.ForEach(x =>
            {
                x.Property("ModifiedDate").CurrentValue = applicationDate;
                x.Property("ModifiedBy").CurrentValue = applicationUserId;
            });
        }
    }
}
