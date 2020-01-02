using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Products
{
    public class ProductGroup: AuditableEntity
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }
        public virtual ICollection<Product> Products { get; private set; }
    }
}
