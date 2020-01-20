using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Supply
{
    public class Receipt: AuditableEntity
    {
        [Required]
        public int ReceiptId { get; set; }
        [Required]
        public string ReceiptName { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public decimal? Price { get; set; }
        [Required]
        public string DocumentNumber { get; set; }
        [Required]
        public WareHouse WareHouse { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ReceiptValue { get; set; }
        public bool Shipped { get; set; }
        public bool Paid { get; set; }
    }
}
