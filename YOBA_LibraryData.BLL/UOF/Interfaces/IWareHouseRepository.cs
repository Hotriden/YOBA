﻿using System.Threading.Tasks;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_LibraryData.BLL.UOF.Interfaces
{
    public interface IWareHouseRepository:IBaseRepository<WareHouse>
    {
        WareHouse GetByName(string name);
        Task Add(WareHouse wareHouse);
    }
}
