using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.SupplyCatalogueFolder;

namespace YOBA_BLL.Catalogue.SupplyCatalogueFolder
{
    public interface ISupplyCatalogue
    {
        ReceiptCatalogue ReceiptCatalogue { get; }
        SupplierCatalogue SupplierCatalogue { get; }
        WareHouseCatalogue WareHouseCatalugue { get; }
    }
}
