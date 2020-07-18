using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF;
using System.Linq;

namespace YOBA_BLL.Supply
{
    public class WareHouseBLL:IWareHouseBLL
    {
        private readonly IUnitOfWork db;
        public WareHouseBLL(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        public string CheckRawStuff(Receipt receipt, WareHouse wareHouse)
        {
            string result = null;
            if (wareHouse !=null & wareHouse.ProductOportunity == true)
            {
                    var presence = (from q in wareHouse.Receipts
                                    where q.Id == receipt.Id
                                    select q.ReceiptValue).First();
                    if (presence >= receipt.ReceiptValue) { }
                    else { result += $"Not anought {receipt.ReceiptName} on {wareHouse.WareHouseName}"; }
                }
            if (result == null)
            {
                return $"{receipt.ReceiptName} successful created";
            }
            else
            {
                return result;
            }
        }
    }
}
