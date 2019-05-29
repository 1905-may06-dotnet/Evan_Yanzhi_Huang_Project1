using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBoxData.data
{
    public partial class PizzaBoxContext : DbContext
    {
        public PizzaBoxContext()
        {
        }

        public PizzaBoxContext(DbContextOptions<PizzaBoxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<PizzaOrder> PizzaOrder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=evanh2019dbserver.database.windows.net;Database=PizzaBoxDb;Server=evanh2019dbserver.database.windows.net;Database=PizzaBoxDb;user id=evanhuang10;Password=Password123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_AppUser_UserID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FullName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.Crust)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Size)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Toppings)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_User_OrderID");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Address)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode).HasMaxLength(10);
            });

            modelBuilder.Entity<PizzaOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_Order_OrderID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.TimeDate)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.PizzaOrder)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Locaiton_LocationID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PizzaOrder)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_UserID");
            });
        }
    }
}
