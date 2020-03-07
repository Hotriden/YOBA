using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.FinanceCatalogueFolder
{
    public class FinanceCatalogue:IFinanceCatalogue
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;
        private ExpenceCatalogue expenceCatalogue;
        private IncomeCatalogue incomeCatalogue;
        private TaxCatalogue taxCatalogue;

        public FinanceCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }

        public ExpenceCatalogue ExpenceCatalogue
        {
            get
            {
                if (expenceCatalogue == null)
                    expenceCatalogue = new ExpenceCatalogue(db, messageService);
                return expenceCatalogue;
            }
        }

        public IncomeCatalogue IncomeCatalogue
        {
            get
            {
                if (incomeCatalogue == null)
                    incomeCatalogue = new IncomeCatalogue(db, messageService);
                return incomeCatalogue;
            }
        }

        public TaxCatalogue TaxCatalogue
        {
            get
            {
                if (taxCatalogue == null)
                    taxCatalogue = new TaxCatalogue(db, messageService);
                return taxCatalogue;
            }
        }
    }
}
