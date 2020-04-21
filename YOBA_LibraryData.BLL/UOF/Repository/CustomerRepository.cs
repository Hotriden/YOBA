using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly YOBAContext _context;
        public CustomerRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(Customer item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Customer item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();

        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers;
        }

        public Customer GetById(int id)
        {
            return _context.Customers.First(customer => customer.CustomerId == id);
        }

        public async Task Change(Customer item)
        {
            _context.Customers.Update(item);
            await _context.SaveChangesAsync();

        }
    }
}
