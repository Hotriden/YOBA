using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_LibraryData.BLL.UOF.Interfaces
{
    public interface IBranchRepository:IBaseRepository<Branch>
    {
        Branch GetByName(string name);
    }
}
