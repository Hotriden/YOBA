using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOBA_Customers.BLL.Entities
{
    public class Payment
    {
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public Order Order { get; set; }
        [Required]
        public Customer Cusmoter { get; set;}
        [Required]
        public DateTime PayTime { get; set; }
    }
}