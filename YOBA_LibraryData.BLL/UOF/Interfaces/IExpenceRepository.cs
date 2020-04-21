﻿using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_LibraryData.BLL.UOF.Interfaces
{
    public interface IExpenceRepository:IBaseRepository<Expence>
    {
        Expence GetByName(string name);
    }
}
