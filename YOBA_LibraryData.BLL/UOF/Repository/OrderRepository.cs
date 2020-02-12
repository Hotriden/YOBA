using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_Services.Exceptions;
using YOBA_LibraryData.DAL;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly YOBAContext _context;
        public OrderRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Order item)
        {
            if (_context.Orders.Find(item.OrderIdentity) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.OrderIdentity);
            }
        }

        public void Delete(Order item)
        {
            if (_context.Orders.First(order => order.OrderIdentity == item.OrderIdentity) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.OrderIdentity);
            }
        }

        public IEnumerable<Order> GetAll()
        {
            if (_context.Orders != null)
            {
                return _context.Orders;
            }
            else
            {
                throw new EmptyDataException(typeof(Payment).ToString());
            }
        }

        public Order GetById(string id)
        {
            var result = _context.Orders.First(order => order.Id == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Order).ToString());
            }
        }

        public Order GetByIdentity(string identity)
        {
            var result = _context.Orders.First(order => order.OrderIdentity == identity);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Order).ToString());
            }
        }

        public void Change(Order item)
        {
            if (_context.Orders.First(order => order.OrderIdentity == item.OrderIdentity) != null)
            {
                _context.Orders.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }
    }
}
