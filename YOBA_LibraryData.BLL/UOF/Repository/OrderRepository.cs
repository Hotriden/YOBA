using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly YOBAContext _context;
        public OrderRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(string userId, Order item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, Order item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll(string userId)
        {
            if (_context.Order != null)
            {
                return _context.Order;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public Order GetById(string userId, int id)
        {
            return _context.Order.First(order => order.Id == id);
        }

        public Order GetByIdentity(string userId, string identity)
        {
            return _context.Order.First(order => order.OrderIdentity == identity); ;
        }

        public async Task Change(string userId, Order item)
        {
            _context.Order.Update(item);
            await _context.SaveChangesAsync();
        }

        public Order Get(string userId, Order item)
        {
            throw new System.NotImplementedException();
        }
    }
}
