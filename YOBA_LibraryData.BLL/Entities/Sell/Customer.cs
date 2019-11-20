using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOBA_LibraryData.BLL.Entities.Sell
{
    public class Customer
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerLastName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public virtual ICollection<Order> CustomerOrders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public string TelephoneNumber { get; set; }
        public string Address { get; set; }
    }
}
