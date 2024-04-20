using E_CommerceAPI.Domain.Eetities;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Domain.Entities.Common;
using E_CommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Contexts 
{
    public class E_CommerceAPIDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public E_CommerceAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.Entity<Order>().HasKey(b => b.Id);
            builder.Entity<Order>().HasIndex(o => o.OrderCode).IsUnique();
            builder.Entity<Basket>().HasOne(b => b.Order).WithOne(o => o.Basket).HasForeignKey<Order>(b => b.Id);
            builder.Entity<Order>().HasOne(o=>o.CompletedOrder).WithOne(o=>o.Order).HasForeignKey<CompletedOrder>(b => b.OrderId);

            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Entityler üzerinde yapılan değişikler yada yeni ekleme verinin yakalanmasını sağlayan propertydir. update operasyonunda track edilen verileri yakalayıp elde etmemizi sağlar 
            var data = ChangeTracker.Entries<BaseEntity>();
            foreach (var item in data)
            {
                _ = item.State switch
                {
                    EntityState.Added => item.Entity.CreateDate = DateTime.UtcNow,
                    EntityState.Modified => item.Entity.UpdateDate = DateTime.UtcNow,
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
