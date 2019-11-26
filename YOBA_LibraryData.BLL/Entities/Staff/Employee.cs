using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOBA_LibraryData.BLL.Entities.Staff
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
        public string Position { get; set; }
        [Required]
        public string TelephoneNumber { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Sallery { get; set; }
    }
}
