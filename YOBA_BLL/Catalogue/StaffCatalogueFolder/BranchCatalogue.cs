using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.StaffCatalogueFolder
{
    public class BranchCatalogue : ICatalogue<Branch>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public BranchCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }

        public void Create(Branch item, string UserId)
        {
            throw new NotImplementedException();
        }

        public void Delete(Branch item, string UserId)
        {
            throw new NotImplementedException();
        }

        public void Update(Branch item, string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
