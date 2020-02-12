using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Finance;

namespace YOBA_BLL.Catalogue.FinanceCatalogueFolder
{
    public interface IFinanceCatalogue
    {
        ExpenceCatalogue ExpenceCatalogue { get; }
        TaxCatalogue TaxCatalogue { get; }
        IncomeCatalogue IncomeCatalogue { get; }
    }
}
