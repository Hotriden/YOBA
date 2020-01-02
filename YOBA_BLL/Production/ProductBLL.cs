using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_BLL.Supply;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Production
{
    public class ProductBLL : IProductBLL
    {
        private IWareHouseBLL _wareHouseBLL;
        private IUnitOfWork UOW;
        public ProductBLL(IUnitOfWork unitOfWork)
        {
            UOW = unitOfWork;
            _wareHouseBLL = new WareHouseBLL();
        }

        public string GetProductName()
        {
            throw new NotImplementedException();
        }

        public string Produce(Product product, WareHouse wareHouse)
        {
            if (wareHouse.ProductOportunity == true && UOW.WareHouseRepository.GetById(wareHouse.Id) != null)
            {
                var result = product.Receipts.Intersect(wareHouse.Receipts);
                if (result == product.Receipts)
                {
                    return $"{product.ProductName} successful created";
                }
                else
                {
                    var falseResult = product.Receipts.Except(wareHouse.Receipts);
                    string distinctResult = null;
                    foreach(var receip in falseResult)
                    {
                        distinctResult += receip;
                    }
                    return distinctResult;
                }
            }
            else
            {
                return "Ware House not able to product or this Ware House does not exist";
            }
        }

        public bool ProductRelocation(Product product, WareHouse wareHouseTo, WareHouse wareHouseFrom)
        {
            throw new NotImplementedException();
        }

        static public bool IsIntersects(IList<int> target, IList<int> second)
        {
            return target.Intersect(second).Count() == target.Count;
        }
    }
}
