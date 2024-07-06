using BellBankingApp.Core.Domain.Common;
using BellBankingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //entidades
        public DbSet<Product> Products { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.DateUpdated = DateTime.Now;
                        entry.Entity.UpdatedBy = "DefaultAppUser";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API

            #region tables
            modelBuilder.Entity<Product>()
                .ToTable("Products");
            modelBuilder.Entity<Beneficiary>()
                .ToTable("Beneficiaries");
            modelBuilder.Entity<Transaction>()
                .ToTable("Transactions");
            #endregion

            #region primar keys
            modelBuilder.Entity<Product>()
                .HasKey(product => product.Id);

            modelBuilder.Entity<Beneficiary>()
                .HasKey(beneficiaries => beneficiaries.Id);

            modelBuilder.Entity<Transaction>()
                .HasKey(transactions => transactions.Id);
            #endregion

            #region "Relationships"
            // Product - User
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.User)
            //    .WithMany(u => u.Products)
            //    .HasForeignKey(p => p.UserId);

            // Beneficiary - Product
            modelBuilder.Entity<Beneficiary>()
                .HasOne(b => b.Product)
                .WithMany(p => p.Beneficiaries)
                .HasForeignKey(b => b.ProductId);

            // Beneficiary - User
            //modelBuilder.Entity<Beneficiary>()
            //    .HasOne(b => b.User)
            //    .WithMany(u => u.Beneficiaries)
            //    .HasForeignKey(b => b.UserId);

            // Transaction - OriginProduct
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.OriginAccount)
                .WithMany()
                .HasForeignKey(t => t.OriginAccount);

            // Transaction - DestinationProduct
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.DestinationProduct)
                .WithMany()
                .HasForeignKey(t => t.DestinationAccount);
            #endregion

            #region
            #region products
            modelBuilder.Entity<Product>().
                Property(product => product.Type)
                .IsRequired()
                .HasMaxLength(100);
            #endregion

            #region beneficiary
            modelBuilder.Entity<Beneficiary>().
                Property(product => product.ProductId)
                .IsRequired();

            modelBuilder.Entity<Beneficiary>().
                Property(product => product.UserId)
                .IsRequired();
            #endregion

            #region transactions
            modelBuilder.Entity<Transaction>().
                Property(transaction => transaction.Amount)
                .IsRequired();
            #endregion
            #endregion
        }
    }
}
