using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using cyberforgepc.Database.Models;

namespace cyberforgepc.Database.Context
{
    public partial class CyberforgepcContext : DbContext
    {
        public CyberforgepcContext()
        {
        }

        public CyberforgepcContext(DbContextOptions<CyberforgepcContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Coupons> Coupons { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WishLists> WishLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\LocalDBA;Database=cyberforgepc;User Id=DONOVAN\\donov;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Coupons>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CouponId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_Orders_Coupons");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Products");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Users");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DeleteKey)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(800);

                entity.Property(e => e.RefreshTimeStamp).HasColumnType("datetime");

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Updated).HasColumnType("datetime");
            });

            modelBuilder.Entity<WishLists>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Updated).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WishLists_Products");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WishLists_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
