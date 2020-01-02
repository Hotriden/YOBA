using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Supply;

namespace YOBA_LibraryData.DAL.Entities.Products
{
    public class RawStuff: AuditableEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string RawStuffName { get; set; }

        [Required]
        public string Measure { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public decimal Price { get; set; }

        public WareHouse Placement { get; set; }
    }
}
