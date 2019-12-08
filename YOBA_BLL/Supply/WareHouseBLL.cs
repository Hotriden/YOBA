using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF;

namespace YOBA_BLL.Supply
{
    class WareHouseBLL:IWareHouseBLL
    {
        private IUnitOfWork UOW;

        public WareHouseBLL()
        {
            UOW = new UnitOfWork();
        }
        public string CheckRawStuff(Product product, WareHouse wareHouse)
        {
            string result = null;
            var _wareHouse = UOW.WareHouseRepository.GetById(wareHouse.Id);
            if (_wareHouse !=null && wareHouse.ProductOportunity == true)
            {
                foreach(var component in product.Receipts)
                {
                    if (_wareHouse.Receipts.Contains(component)) { }
                    else { result += $"Not anought {_wareHouse.Receipts} on {_wareHouse.WareHouseName}"; }
                }
            }
            return result;
        }
    }
}
