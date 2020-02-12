using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Sell
{
    public class Customer: AuditableEntity
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerLastName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public virtual ICollection<Order> CustomerOrders { get; private set; }
        public virtual ICollection<Payment> Payments { get; private set; }
        public string TelephoneNumber { get; set; }
        public string Address { get; set; }
    }
}
