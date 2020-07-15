using Microsoft.EntityFrameworkCore;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.DAL.Entities;
using YOBA_LibraryData.DAL.Entities.User;

namespace YOBA_LibraryData.DAL
{
    public class YOBAContext:DbContext
    { 
        public YOBAContext(DbContextOptions<YOBAContext> options) 
            : base(options) 
        {
            Database.EnsureCreated(); 
        }

        public virtual DbSet<Expence> Expences { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<WareHouse> WareHouses { get; set; }
        public virtual DbSet<UserLog> ClientLogs { get; set; }
        public virtual DbSet<UserModel> UserModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expence>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WareHouse>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductOportunity)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });
        }
    }
}
