using System;
using System.Collections.Generic;
using System.Text;
using YOBA_BLL.Catalogue.FinanceCatalogueFolder;
using YOBA_BLL.Catalogue.ProductCatalogueFolder;
using YOBA_BLL.Catalogue.SellCatalogueFolder;
using YOBA_BLL.Catalogue.StaffCatalogueFolder;
using YOBA_BLL.Catalogue.SupplyCatalogueFolder;

namespace YOBA_BLL.Catalogue
{
    public class YobaCatalogue : IYobaCatalogue
    {
        //public ISupplyCatalogue SupplyCatalogue
        //{
        //    get
        //    {
        //        //return new SupplyCatalogue();
        //    }
        //}

        public IStaffCatalogue StaffCatalogue => throw new NotImplementedException();

        public ISellCatalogue SellCatalogue => throw new NotImplementedException();

        public IProductCatalogue ProductCatalogue => throw new NotImplementedException();

        public IFinanceCatalogue FinanceCatalogue => throw new NotImplementedException();

        public ISupplyCatalogue SupplyCatalogue => throw new NotImplementedException();
    }
}
