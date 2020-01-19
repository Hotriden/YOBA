using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Interfaces;
using YOBA_LibraryData.BLL.UOF.Interfaces;

namespace YOBA_BLL.Catalogue.SupplyCatalogueFolder
{
    public class WareHouseCatalogue : ICatalogue<WareHouse>
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;

        public WareHouseCatalogue(IUnitOfWork _repo, IMessageService service)
        {
            messageService = service;
            db = _repo;
        }

        public void Create(WareHouse branch, string UserId)
        {
            throw new NotImplementedException();
        }

        public void Delete(WareHouse branch, string UserId)
        {
            throw new NotImplementedException();
        }

        public void Update(WareHouse branch, string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
