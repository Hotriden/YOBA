using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_Services.Exceptions;
using YOBA_LibraryData.DAL;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly YOBAContext _context;
        public WareHouseRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(WareHouse item)
        {
            if (_context.WareHouses.Find(item.WareHouseName) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.WareHouseName);
            }
        }

        public void Delete(WareHouse item)
        {
            if (_context.WareHouses.First(warehouse => warehouse.Id == item.Id) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }

        public IEnumerable<WareHouse> GetAll()
        {
            if (_context.WareHouses != null)
            {
                return _context.WareHouses;
            }
            else
            {
                throw new EmptyDataException(typeof(WareHouse).ToString());
            }
        }

        public WareHouse GetById(string id)
        {
            var result = _context.WareHouses.First(warehouse => warehouse.Id == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(WareHouse).ToString());
            }
        }

        public WareHouse GetByName(string name)
        {
            var result = _context.WareHouses.First(warehouse => warehouse.WareHouseName == name);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(WareHouse).ToString());
            }
        }

        public void Change(WareHouse item)
        {
            if (_context.WareHouses.First(x => x.Id == item.Id) != null)
            {
                _context.WareHouses.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Id);
            }
        }
    }
}
