using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;
using YOBA_LibraryData.DAL.UOF;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly YOBAContext _context;
        public CustomerRepository(YOBAContext context)
        {
            _context = context;
        }

        public IQueryable<Customer> GetAll(string userId)
        {
            return _context.Customers;
        }

        public async Task Add(string userId, Customer item)
        {
            if (item != null)
            {
                item.OnAdd(userId);
                _context.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        public Customer Get(string userId, Customer item)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(string userId, Customer item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Change(string userId, Customer item)
        {
            _context.Customers.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
