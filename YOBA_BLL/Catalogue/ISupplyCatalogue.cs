using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Supply;

namespace YOBA_BLL.Catalogue
{
    public interface ICatalogue<T>
    {
        void Create(T branch, string UserId);
        void Update(T branch, string UserId);
        void Delete(T branch, string UserId);
    }
}
