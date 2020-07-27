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
            else
            {
                throw new EntityException("Warehouse data error");
            }
        }

        public async Task Delete(string userId, WareHouse wareHouse)
        {
            if (_context.WareHouses.Where(user => user.UserId == userId).Where(wh => wh.Id == wareHouse.Id) != null)
            {
                _context.Remove(wareHouse);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityException("Warehouse not found");
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
            else
            {
                throw new EntityException("Warehouse not found");
            }
        }
        /// <summary>
        /// Can include any of warehouse
        /// parameters to find entity
        /// on database for exceptional user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public WareHouse Get(string userId, WareHouse item)
        {
            WareHouse wareHouse = item;
            bool isExist = _context.WareHouses.Any(c => c.Id == wareHouse.Id);
            if (isExist)
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
            throw new EntityException("Warehouse not found");
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