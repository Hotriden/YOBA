using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Supply;

namespace YOBA_BLL.Supply
{
    public interface IWareHouseBLL
    {
        string CheckRawStuff(Receipt product, WareHouse wareHouser);
    }
}
