using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace YOBA_LibraryData.BLL.UOF.Repository
{
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly YOBAContext _context;
        public WareHouseRepository(YOBAContext context)
        {
            _context = context;
        }
        public async Task Add(WareHouse item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            _context.WareHouses.All(c => c.LastModifiedBy == "");
        }

        public async Task Delete(WareHouse item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<WareHouse> GetAll()
        {
            return _context.WareHouses;
        }

        public WareHouse GetById(string id)
        {
            var result = _context.WareHouses.Find(id); ;
            return result;
        }

        public WareHouse GetByEmail(string name)
        {
            return _context.WareHouses.First(warehouse => warehouse.WareHouseName == name);
        }

        public async Task Change(WareHouse item)
        {
            _context.WareHouses.Update(item);
            await _context.SaveChangesAsync();
        }

        public WareHouse GetWareHouse(WareHouse wareHouse)
        {
            throw new System.NotImplementedException();
        }

        public WareHouse GetWareHouseByUser(IdentityUser user)
        {
            throw new System.NotImplementedException();
        }

        public WareHouse GetWareHouseByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public WareHouse GetWareHouseByAddress(string address)
        {
            throw new System.NotImplementedException();
        }

        public WareHouse GetWareHouseByStockMan(int stockManId)
        {
            throw new System.NotImplementedException();
        }

        public WareHouse GetWareHouseByProductOportunitu(bool productOportunity)
        {
            throw new System.NotImplementedException();
        }

        public WareHouse GetWareHouseByReceipt(Receipt receipt)
        {
            throw new System.NotImplementedException();
        }

        public WareHouse GetWareHouseByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<WareHouse> GetAll(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task Add(string userId, WareHouse item)
        {
            throw new System.NotImplementedException();
        }

        public WareHouse GetById(string userId, int id)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string userId, WareHouse item)
        {
            throw new System.NotImplementedException();
        }

        public Task Change(string userId, WareHouse item)
        {
            throw new System.NotImplementedException();
        }
    }
}
