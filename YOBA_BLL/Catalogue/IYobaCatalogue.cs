using YOBA_BLL.Catalogue.FinanceCatalogueFolder;
using YOBA_BLL.Catalogue.ProductCatalogueFolder;
using YOBA_BLL.Catalogue.SellCatalogueFolder;
using YOBA_BLL.Catalogue.StaffCatalogueFolder;
using YOBA_BLL.Catalogue.SupplyCatalogueFolder;

namespace YOBA_BLL.Catalogue
{
    public interface IYobaCatalogue
    {
        ISupplyCatalogue SupplyCatalogue { get; }
        IStaffCatalogue StaffCatalogue { get; }
        ISellCatalogue SellCatalogue { get; }
        IProductCatalogue ProductCatalogue { get; }
        IFinanceCatalogue FinanceCatalogue { get; }
    }
}
