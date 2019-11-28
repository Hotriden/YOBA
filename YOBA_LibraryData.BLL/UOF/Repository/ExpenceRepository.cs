using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class ExpenceRepository : IExpenceRepository
    {
        private YOBAContext _context;
        public ExpenceRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Expence item)
        {
            if (_context.Expences.Find(item.Name) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.Name);
            }
        }

        public void Delete(Expence item)
        {
            if (_context.Expences.First(expence => expence.Id == item.Id) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }

        public IEnumerable<Expence> GetAll()
        {
            if (_context.Expences != null)
            {
                return _context.Expences;
            }
            else
            {
                throw new EmptyDataException(typeof(Expence).ToString());
            }
        }

        public Expence GetById(int id)
        {
            var result = _context.Expences.First(expence => expence.Id == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Expence).ToString());
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Change(Expence item)
        {
            if (_context.Expences.First(x => x.Id == item.Id) != null)
            {
                _context.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }
    }
}
