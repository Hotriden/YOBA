using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YOBA_LibraryData.BLL.Entities.Sell;

namespace YOBA_LibraryData.BLL.Entities.Supply
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Payment> Payments { get; private set; }
        public virtual ICollection<Receipt> Entrances { get; private set; }
        public string TelephoneNumber { get; set; }
    }
}