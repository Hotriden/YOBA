using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_BLL.Catalogue.SellCatalogueFolder
{
    public interface ISellCatalogue
    {
        CustomerCatalogue CustomerCatalogue { get; }
        OrderCatalogue OrderCatalogue { get; }
        PaymentCatalogue PaymentCatalogue { get; }
    }
}
