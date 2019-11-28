using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_LibraryData.BLL.UOF.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Order GetByIdentity(string identity);
    }
}
