using System;
using System.Collections.Generic;
using System.Text;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.DAL.UOF
{
    public static class AuditableExtention
    {
        public static void OnAdd(this AuditableEntity entity, string userId)
        {
            entity.Created = DateTime.Now;
            entity.LastModified = entity.Created;
            entity.CreatedBy = entity.LastModifiedBy = userId;
        }

        public static void OnChange(this AuditableEntity entity, string userId)
        {
            entity.LastModified = DateTime.Now;
            entity.LastModifiedBy = userId;
        }
    }
}
