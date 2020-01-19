using System;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.BLL.UOF.Repository;
using YOBA_LibraryData.DAL.UOF.Interfaces;
using YOBA_LibraryData.DAL.UOF.Repository;

namespace YOBA_LibraryData.BLL.UOF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly YOBAContext db;
        private BranchRepository branchRepo;
        private CustomerRepository customerRepo;
        private EmployeeRepository employeeRepo;
        private ExpenceRepository expenceRepo;
        private IncomeRepository incomeRepo;
        private OrderRepository orderRepo;
        private PaymentRepository paymentRepo;
        private SupplierRepository supplierRepo;
        private TaxRepository taxRepo;
        private WareHouseRepository wareHouseRepo;
        private ClientLogRepository clientLogRepo;
        private ReceiptRepository receiptRepo;

        public UnitOfWork()
        {
            db = new YOBAContext(); /// connection string
        }

        public IBranchRepository BranchRepository
        {
            get
            {
                if (branchRepo == null)
                    branchRepo = new BranchRepository(db);
                return branchRepo;
            }
        }

        public ICustomerRepository CustomerRepository 
        {
            get 
            {
                if (customerRepo == null)
                    customerRepo = new CustomerRepository(db);
                return customerRepo;
            }
        }
        public IEmployeeRepository EmployeeRepository 
        {
            get
            {
                if (employeeRepo == null)
                    employeeRepo = new EmployeeRepository(db);
                return employeeRepo;
            }
        }
        public IExpenceRepository ExpenceRepository 
        { 
            get
            {
                if (expenceRepo == null)
                    expenceRepo = new ExpenceRepository(db);
                return expenceRepo;
            }
        }
        public IIncomeRepository IncomeRepository 
        {
            get
            {
                if (incomeRepo == null)
                    incomeRepo = new IncomeRepository(db);
                return incomeRepo;
            }
        }
        public IOrderRepository RrderRepository 
        {
            get 
            {
                if (orderRepo == null)
                    orderRepo = new OrderRepository(db);
                return orderRepo;
            }
        }
        public IPaymentRepository PaymentRepository 
        {
            get 
            {
                if (paymentRepo == null)
                    paymentRepo = new PaymentRepository(db);
                return paymentRepo;
            }

        }

        public ISupplierRepository SupplierRepository
        {
            get
            {
                if (supplierRepo == null)
                    supplierRepo = new SupplierRepository(db);
                return supplierRepo;
            }
        }
        public ITaxRepository TaxRepository 
        {
            get 
            {
                if (taxRepo == null)
                    taxRepo = new TaxRepository(db);
                return taxRepo;
            }
        }
        public IWareHouseRepository WareHouseRepository 
        {
            get 
            {
                if (wareHouseRepo == null)
                    wareHouseRepo = new WareHouseRepository(db);
                return wareHouseRepo;
            }
        }

        public IClientLogRepository ClientLogRepository
        {
            get
            {
                if (clientLogRepo == null)
                    clientLogRepo = new ClientLogRepository(db);
                return clientLogRepo;
            }
        }

        public IReceiptRepository ReceiptRepository
        {
            get
            {
                if (receiptRepo == null)
                    receiptRepo = new ReceiptRepository(db);
                return receiptRepo;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (orderRepo == null)
                    orderRepo = new OrderRepository(db);
                return orderRepo;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
