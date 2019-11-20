using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOBA_LibraryData.BLL.Entities.Products
{
    public class ProductGroup
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        public string GropName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
