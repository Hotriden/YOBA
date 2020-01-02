using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_LibraryData.BLL.UOF.Interfaces
{
    public interface ISupplierRepository:IBaseRepository<Supplier>
    {
        Supplier GetByNumber(string identity);
    }
}
