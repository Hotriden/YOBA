using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Supply;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Products
{
    public class Product: AuditableEntity
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Column(TypeName="decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        public WareHouse Placement { get; set; }

        public double ProductTime { get; set; }
        public ICollection<Receipt> Receipts { get; set; }
    }
}
