using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOBA_LibraryData.BLL.Entities.Staff
{
    public class Branch
    {
        [Required]
        public int BranchId { get; set; }
        [Required]
        public string BranchName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
