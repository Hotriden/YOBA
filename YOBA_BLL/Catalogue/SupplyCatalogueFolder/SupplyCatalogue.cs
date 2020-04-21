using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_BLL.SupplyCatalogueFolder;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.SupplyCatalogueFolder
{
    public class SupplyCatalogue:ISupplyCatalogue
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;
        private ReceiptCatalogue receiptCatalogue;
        private WareHouseCatalogue wareHouseCatalogue;
        private SupplierCatalogue supplierCatalogue;
        public SupplyCatalogue(IUnitOfWork _uof, IMessageService _messageService)
        {
            db = _uof;
            messageService = _messageService;
        }

        public ReceiptCatalogue ReceiptCatalogue
        {
            get
            {
                if (receiptCatalogue == null)
                    receiptCatalogue = new ReceiptCatalogue(db, messageService);
                return receiptCatalogue;
            }
        }
        public WareHouseCatalogue WareHouseCatalugue
        {
            get
            {
                if (wareHouseCatalogue == null)
                    wareHouseCatalogue = new WareHouseCatalogue(db, messageService);
                return wareHouseCatalogue;
            }
        }

        public SupplierCatalogue SupplierCatalogue
        {
            get
            {
                if (supplierCatalogue == null)
                    supplierCatalogue = new SupplierCatalogue(db, messageService);
                return supplierCatalogue;
            }
        }
    }
}
