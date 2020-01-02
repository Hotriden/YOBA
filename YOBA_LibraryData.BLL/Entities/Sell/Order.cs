using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Sell
{
    public class Order: AuditableEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public Employee Manager { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderSum { get; set; }
        public bool Shipped { get; set; }
        public bool Paid { get; set; }
        public string OrderIdentity { get; set; }
    }
}
