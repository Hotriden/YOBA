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
        public async Task Add(Order item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Order item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Order> GetAll()
        {
            if (_context.Orders != null)
            {
                return _context.Orders;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public Order GetById(int id)
        {
            return _context.Orders.First(order => order.Id == id);
        }

        public Order GetByIdentity(string identity)
        {
            return _context.Orders.First(order => order.OrderIdentity == identity); ;
        }

        public async Task Change(Order item)
        {
            _context.Orders.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
