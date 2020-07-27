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
            //Database.EnsureCreated(); 
        }

        public virtual DbSet<Expence> Expence { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<Tax> Tax { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Receipt> Receipt { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<WareHouse> WareHouse { get; set; }
        public virtual DbSet<UserLog> ClientLog { get; set; }
        public virtual DbSet<UserModel> UserModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

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
