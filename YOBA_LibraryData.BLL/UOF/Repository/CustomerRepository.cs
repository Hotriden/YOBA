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

        public IQueryable<Customer> GetAll(string userId)
        {
            return _context.Customers;
        }

        public Customer GetById(int id)
        {
            return _context.Customers.First(customer => customer.Id == id);
        }

        public async Task Change(Customer item)
        {
            _context.Customers.Update(item);
            await _context.SaveChangesAsync();
        }

        public Task Add(string userId, Customer item)
        {
            throw new System.NotImplementedException();
        }

        public Customer GetById(string userId, int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string userId, Customer item)
        {
            throw new System.NotImplementedException();
        }

        public Task Change(string userId, Customer item)
        {
            throw new System.NotImplementedException();
        }
    }
}
