using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Staff
{
    public class Branch: AuditableEntity
    {
        [Required]
        public string BranchId { get; set; }
        [Required]
        public string BranchName { get; set; }
        public virtual ICollection<Employee> Employees { get; private set; }
    }
}
