

using System;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF.Interfaces;
using YOBA_LibraryData.BLL.UOF.Repository;

namespace YOBA_LibraryData.BLL.UOF
{
    public class UnitOfWork : IUnitOfWork
    {
        private YOBAContext db;
        private BranchRepository branchRepo;
        private CustomerRepository customerRepo;
        private EmployeeRepository employeeRepo;
        private ExpenceRepository expenceRepo;
        private IncomeRepository incomeRepo;
        private OrderRepository orderRepo;
        private PaymentRepository paymentRepo;
        private ProductGroupRepository productGroupRepo;
        private ProductRepository productRepo;
        private ReceiptRepository receiptRepo;
        private SupplierRepository supplierRepo;
        private TaxRepository taxRepo;
        private WareHouseRepository wareHouseRepo;

        public UnitOfWork(string connectionString)
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
        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepo == null)
                    productRepo = new ProductRepository(db);
                return productRepo;
            }
        }
        public IProductGroupRepository ProductGroupRepository 
        {
            get 
            {
                if (productGroupRepo == null)
                    productGroupRepo = new ProductGroupRepository(db);
                return productGroupRepo;
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
