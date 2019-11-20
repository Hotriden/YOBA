using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Products;

namespace YOBA_LibraryData.BLL.Entities.Supply
{
    public class Entrance
    {
        [Required]
        public int EntranceId { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public WareHouse WareHouse { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderSum { get; set; }
        public bool Shipped { get; set; }
        public bool Paid { get; set; }
    }
}
