using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Catalogue.FinanceCatalogueFolder;
using YOBA_BLL.Catalogue.SellCatalogueFolder;
using YOBA_BLL.Catalogue.StaffCatalogueFolder;
using YOBA_BLL.Catalogue.SupplyCatalogueFolder;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue
{
    public class YobaCatalogue : IYobaCatalogue
    {
        private IUnitOfWork _UOF;
        private IMessageService _messageService;
        public YobaCatalogue(IUnitOfWork unitOfWork, IMessageService messageService)
        {
            _UOF = unitOfWork;
            _messageService = messageService;
        }
        public ISupplyCatalogue SupplyCatalogue
        {
            get
            {
                return new SupplyCatalogue(_UOF, _messageService);
            }
        }

        public IStaffCatalogue StaffCatalogue
        {
            get
            {
                return new StaffCatalogue(_UOF, _messageService);
            }
        }

        public ISellCatalogue SellCatalogue => throw new NotImplementedException();


        public IFinanceCatalogue FinanceCatalogue => throw new NotImplementedException();

    }
}
