using System;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.DAL.UOF.Interfaces;

namespace YOBA_LibraryData.BLL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBranchRepository BranchRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IExpenceRepository ExpenceRepository { get; }
        IIncomeRepository IncomeRepository { get; }
        IOrderRepository RrderRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductGroupRepository ProductGroupRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        ITaxRepository TaxRepository { get; }
        IWareHouseRepository WareHouseRepository { get; }
        IClientLogRepository ClientLogRepository { get; }
        void Save();
    }
}
