using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.StaffCatalogueFolder
{
    public class StaffCatalogue : IStaffCatalogue
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;
        private BranchCatalogue branchCatalogue;
        private EmployeeCatalogue employeeCatalogue;

        public StaffCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }

        public BranchCatalogue BranchCatalogue
        {
            get
            {
                if (branchCatalogue == null)
                    branchCatalogue = new BranchCatalogue(db, messageService);
                return branchCatalogue;
            }
        }
        public EmployeeCatalogue EmployeeCatalogue
        {
            get
            {
                if (employeeCatalogue == null)
                    employeeCatalogue = new EmployeeCatalogue(db, messageService);
                return employeeCatalogue;
            }
        }
    }
}
