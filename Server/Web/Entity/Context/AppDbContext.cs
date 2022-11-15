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
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockDetail> StockDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<DailyProductDetail> DailyProductDetails { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<DineInTable> DineInTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("public");
            //modelBuilder.Entity<User>().ToTable("users");

            UserRelations(modelBuilder);
            StockRelations(modelBuilder);
            ProductRelations(modelBuilder);
            OrderRelations(modelBuilder);
            DineInRelations(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void UserRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.UserType)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserTypeId);
        }
        private void StockRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .HasMany(x => x.StockDetails)
                .WithOne(x => x.Stock)
                .HasForeignKey(x => x.StockId);
            modelBuilder.Entity<Stock>()
                .HasMany(x => x.ProductStocks)
                .WithOne(x => x.Stock)
                .HasForeignKey(x => x.StockId);
        }
        private void ProductRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(x => x.ProductStocks)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Product>()
                .HasMany(x => x.OrderDetails)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Product>()
                .HasMany(x => x.DailyProductDetails)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
        }

        private void OrderRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderDetails)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
            
        }

        private void DineInRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DineInTable>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.DineInTable)
                .HasForeignKey(x => x.DineInTableId);
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
