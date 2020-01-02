using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Supply
{
    public class WareHouse: AuditableEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string WareHouseName { get; set; }
        [Required]
        public string Address { get; set; }
        public Employee StockMan { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Receipt> Receipts { get; set; }
        public bool ProductOportunity { get; set; }
    }
}
