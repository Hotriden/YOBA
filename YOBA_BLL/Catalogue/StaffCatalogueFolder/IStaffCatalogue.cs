﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_BLL.Catalogue.StaffCatalogueFolder
{
    public interface IStaffCatalogue
    {
        BranchCatalogue BranchCatalogue { get; }
        EmployeeCatalogue EmployeeCatalogue { get; }
    }
}
