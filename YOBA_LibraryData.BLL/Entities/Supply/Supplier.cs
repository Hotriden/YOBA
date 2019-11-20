using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOBA_LibraryData.BLL.Entities.Sell;

namespace YOBA_LibraryData.BLL.Entities.Supply
{
    public class Supplier
    {
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Entrance> Entrances { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
