using Microsoft.EntityFrameworkCore;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Entities.User;

namespace YOBA_LibraryData.BLL
{
    public class YOBAContext:DbContext
    {
        public YOBAContext() { }

        public YOBAContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Expence> Expences { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
    }
}
