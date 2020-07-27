using System.Linq;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;
using YOBA_LibraryData.DAL.UOF;
using System;
using YOBA_LibraryData.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

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
            if (_context.WareHouse.Where(user => user.UserId == userId).First(wh => wh.Id == wareHouse.Id) != null)
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
            var result = _context.WareHouse.Where(c => c.UserId == userId);
            return result;
        }

        public async Task Change(string userId, WareHouse wareHouse)
        {
            var wh = _context.WareHouse.Where(user => user.UserId == userId).First(wh => wh.Id == wareHouse.Id);
            if (wareHouse != null)
            {
                _context.Entry(wh).CurrentValues.SetValues(wareHouse);
            }
            await _context.SaveChangesAsync();
            //throw new EntityException("Warehouse not found");
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
            bool isExist = _context.WareHouse.Any(c => c.Id == wareHouse.Id);
            if (isExist)
            {
                wareHouse = _context.WareHouse.First(c => c.Id == wareHouse.Id);
                return wareHouse;
            }
            if (!string.IsNullOrEmpty(wareHouse.WareHouseName))
            {
                wareHouse = _context.WareHouse.First(c => c.WareHouseName == wareHouse.WareHouseName);
                return wareHouse;
            }
            if (!string.IsNullOrEmpty(wareHouse.Address))
            {
                wareHouse = _context.WareHouse.First(c => c.Address == wareHouse.Address);
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