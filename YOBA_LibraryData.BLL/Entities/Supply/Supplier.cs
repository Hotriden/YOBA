using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YOBA_LibraryData.BLL.Entities.Sell;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Supply
{
    public class Supplier: AuditableEntity
    {
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Payment> Payments { get; private set; }
        public virtual ICollection<Receipt> Entrances { get; private set; }
        public string TelephoneNumber { get; set; }
    }
}