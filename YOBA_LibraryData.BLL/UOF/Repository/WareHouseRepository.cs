using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL;
using System.Threading.Tasks;
using YOBA_LibraryData.BLL.Interfaces;

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

        public WareHouse GetById(int id)
        {
            var result = _context.WareHouses.Find(id); ;
            return result;
        }

        public WareHouse GetByName(string name)
        {
            return _context.WareHouses.First(warehouse => warehouse.WareHouseName == name);
        }

        public async Task Change(WareHouse item)
        {
            _context.WareHouses.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
