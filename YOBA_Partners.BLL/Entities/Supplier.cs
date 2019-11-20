using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOBA_Supplier.BLL.Entities
{
    public class Supplier
    {
        [Required]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
    }
}
