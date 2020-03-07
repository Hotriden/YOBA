using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Services.MessageService;
using YOBA_LibraryData.BLL.Interfaces;

namespace YOBA_BLL.Catalogue.SellCatalogueFolder
{
    public class SellCatalogue:ISellCatalogue
    {
        private readonly IUnitOfWork db;
        private readonly IMessageService messageService;
        private OrderCatalogue orderCatalogue;
        private PaymentCatalogue paymentCatalogue;
        private CustomerCatalogue customerCatalogue;

        public SellCatalogue(IUnitOfWork UOF, IMessageService _messageService)
        {
            db = UOF;
            messageService = _messageService;
        }

        public CustomerCatalogue CustomerCatalogue
        {
            get
            {
                if (customerCatalogue == null)
                    customerCatalogue = new CustomerCatalogue(db, messageService);
                return customerCatalogue;
            }
        }

        public OrderCatalogue OrderCatalogue
        {
            get
            {
                if (orderCatalogue == null)
                    orderCatalogue = new OrderCatalogue(db, messageService);
                return orderCatalogue;
            }
        }

        public PaymentCatalogue PaymentCatalogue
        {
            get
            {
                if (paymentCatalogue == null)
                    paymentCatalogue = new PaymentCatalogue(db, messageService);
                return paymentCatalogue;
            }
        }
    }
}
