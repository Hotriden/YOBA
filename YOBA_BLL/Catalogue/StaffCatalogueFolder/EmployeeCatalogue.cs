using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.StaffCatalogueFolder
{
    public class EmployeeCatalogue : ICatalogue<Employee>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public EmployeeCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }

        public void Create(Employee item, string UserId)
        {
            throw new NotImplementedException();
        }

        public void Delete(Employee item, string UserId)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee item, string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
