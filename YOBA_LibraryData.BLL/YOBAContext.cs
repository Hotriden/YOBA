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
        public YOBAContext(DbContextOptions options) : base(options) { }

        public DbSet<Expence> Expences { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
