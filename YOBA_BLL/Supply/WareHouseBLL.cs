using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF;
using System.Linq;

namespace YOBA_BLL.Supply
{
    public class WareHouseBLL:IWareHouseBLL
    {
        IUnitOfWork db;
        public WareHouseBLL()
        {
            db = new UnitOfWork();
        }
        public string CheckRawStuff(Product product, WareHouse wareHouse)
        {
            string result = null;
            if (wareHouse !=null & wareHouse.ProductOportunity == true)
            {
                foreach(var component in product.Receipts)
                {
                    var presence = (from q in wareHouse.Receipts
                                    where q.ReceiptId == component.ReceiptId
                                    select q.ReceiptValue).First();
                    if (presence >= component.ReceiptValue) { }
                    else { result += $"Not anought {product.Receipts} on {wareHouse.WareHouseName}"; }
                }
            }
            if (result == null)
            {
                return $"{product.ProductName} successful created";
            }
            else
            {
                return result;
            }
        }
    }
}
