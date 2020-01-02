using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Entities.User;

namespace YOBA_BLL.Supply
{
    public interface IWareHouseBLL
    {
        string CheckRawStuff(Product product, WareHouse wareHouser);
    }
}
