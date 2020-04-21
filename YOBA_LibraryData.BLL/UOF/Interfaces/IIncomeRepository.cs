using YOBA_LibraryData.BLL.Entities.Finance;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_LibraryData.BLL.UOF.Interfaces
{
    public interface IIncomeRepository:IBaseRepository<Income>
    {
        Income GetByName(string name);
    }
}
