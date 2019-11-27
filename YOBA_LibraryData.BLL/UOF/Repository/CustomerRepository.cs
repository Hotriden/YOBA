using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class CustomerRepository : IBaseRepository<Customer>
    {
        private YOBAContext _context;
        public CustomerRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Customer item)
        {
            if (_context.Customers.Find(item.CustomerEmail) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.CustomerName);
            }
        }

        public void Delete(Customer item)
        {
            if (_context.Customers.First(customer => customer.CustomerId == item.CustomerId) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.CustomerId);
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            if (_context.Customers != null)
            {
                return _context.Customers;
            }
            else
            {
                throw new EmptyDataException(typeof(Customer).ToString());
            }
        }

        public Customer GetById(int id)
        {
            var result = _context.Customers.First(customer => customer.CustomerId == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Customer).ToString());
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Change(Customer item)
        {
            if (_context.Customers.First(x => x.CustomerId == item.CustomerId) != null)
            {
                _context.Customers.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.CustomerId);
            }
        }
    }
}
