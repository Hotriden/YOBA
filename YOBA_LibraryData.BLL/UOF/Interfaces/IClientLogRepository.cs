using YOBA_LibraryData.BLL.Entities.User;

namespace YOBA_LibraryData.DAL.UOF.Interfaces
{
    public interface IClientLogRepository
    {
        void AddClientChanges(object obj, Client client, string message);
    }
}
