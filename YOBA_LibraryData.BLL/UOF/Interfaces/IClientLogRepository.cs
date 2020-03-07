namespace YOBA_LibraryData.DAL.UOF.Interfaces
{
    public interface IClientLogRepository
    {
        void AddClientChanges(object obj, string UserId, string message);
    }
}
