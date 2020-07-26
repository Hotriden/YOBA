using System.Linq;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;
using YOBA_LibraryData.DAL.UOF;
using System;
using YOBA_LibraryData.DAL.Exceptions;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly YOBAContext _context;
        public WareHouseRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(string userId, WareHouse wareHouse)
        {
            if (wareHouse != null)
            {
                wareHouse.OnAdd(userId);
                _context.Add(wareHouse);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(string userId, WareHouse item)
        {
            if (_context.WareHouses.Where(user => user.UserId == userId).Where(wh => wh.Id == item.Id) != null)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<WareHouse> GetAll(string userId)
        {
            var result = _context.WareHouses.Where(c => c.UserId == userId);
            return result;
        }

        public async Task Change(string userId, WareHouse wareHouse)
        {
            if (wareHouse != null)
            {
                wareHouse.OnChange(userId);
                _context.WareHouses.Update(wareHouse);
                await _context.SaveChangesAsync();
            }
        }

        public WareHouse Get(string userId, WareHouse item)
        {
            WareHouse wareHouse = item;
            bool exist = _context.WareHouses.Any(c => c.Id == wareHouse.Id);
            if (exist)
            {
                wareHouse = _context.WareHouses.First(c => c.Id == wareHouse.Id);
                return wareHouse;
            }
            if (!string.IsNullOrEmpty(wareHouse.WareHouseName))
            {
                wareHouse = _context.WareHouses.First(c => c.WareHouseName == wareHouse.WareHouseName);
                return wareHouse;
            }
            if (!string.IsNullOrEmpty(wareHouse.Address))
            {
                wareHouse = _context.WareHouses.First(c => c.Address == wareHouse.Address);
                return wareHouse;
            }
            throw new EntityException("Ware house not found");
        }

        public WareHouse GetByReceipt(string userId, Receipt receipt)
        {
            throw new NotImplementedException();
        }

        public WareHouse GetByProductOportunity(string userId, bool oportunity)
        {
            throw new NotImplementedException();
        }
    }
}