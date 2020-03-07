using Microsoft.EntityFrameworkCore;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.DAL
{
    public class YOBAContext:DbContext
    { 
        public YOBAContext(DbContextOptions<YOBAContext> options) 
            : base(options) 
        {
            Database.EnsureCreated(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WareHouse>().Property(e => e.Id).ValueGeneratedOnAdd();
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
    }
}
