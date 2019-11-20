using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOBA_Employee.BLL.Entities
{
    public class Employee
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Branch Branch { get; set; }
        [Required]
        public Position Position { get; set; }
        [Required]
        public string TelephoneNumber { get; set; }
        [Required]
        public decimal Sallery { get; set; }
    }
}
