using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_LibraryData.BLL.UOF.Interfaces
{
    public interface IWareHouseRepository:IBaseRepository<WareHouse>
    {
        WareHouse GetWareHouse(WareHouse wareHouse);
        WareHouse GetWareHouseByUser(IdentityUser user);
        WareHouse GetWareHouseByName(string name);
        WareHouse GetWareHouseByAddress(string address);
        WareHouse GetWareHouseByStockMan(int stockManId);
        WareHouse GetWareHouseByProductOportunitu(bool productOportunity);
        WareHouse GetWareHouseByReceipt(Receipt receipt);
        WareHouse GetWareHouseByEmail(string email);
    }
}
