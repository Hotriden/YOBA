using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YOBA_LibraryData.BLL.Entities.Staff;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Supply
{
    public class WareHouse: AuditableEntity
    {
        [Required]
        public string WareHouseName { get; set; }
        public string Address { get; set; }
        public Employee StockMan { get; set; }
        public IEnumerable<Receipt> Receipts { get; set; }
        public bool ProductOportunity { get; set; }
    }
}
