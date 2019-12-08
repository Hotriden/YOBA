using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Supply;

namespace YOBA_BLL.Production
{
    public interface IProductBLL
    {
        string Produce(Product product, WareHouse wareHouse);
        string GetProductName();
        bool ProductRelocation(Product product, WareHouse wareHouseFrom, WareHouse wareHouseTo);
    }
}
