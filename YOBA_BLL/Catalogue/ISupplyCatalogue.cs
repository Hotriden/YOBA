using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.BLL.Entities.User;

namespace YOBA_BLL.Catalogue
{
    public interface ISupplyCatalogue
    {
        void CreateSupplier(Supplier suppier, Client client);
        void ChangeSupplier(Supplier supplier, Client client);
        void DeleteSupplier(Supplier supplier, Client client);
    }
}
