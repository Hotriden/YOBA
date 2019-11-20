using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOBA_Employee.BLL.Entities;
using YOBA_Products.BLL.Entities;

namespace YOBA_Customers.BLL.Entities
{
    public class Order
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public Employee Manager { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        public decimal OrderSum { get; set; }
        public bool Shipped { get; set; }
        public bool Paid { get; set; }
    }
}
