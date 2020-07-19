using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_LibraryData.BLL.UOF.Interfaces
{
    public interface IWareHouseRepository:IBaseRepository<WareHouse>
    {
        WareHouse GetByReceipt(string userId, Receipt receipt);
        WareHouse GetByProductOportunity(string userId, bool oportunity);
    }
}
