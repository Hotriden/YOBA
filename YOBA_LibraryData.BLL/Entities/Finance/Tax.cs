using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Finance
{
    public class Tax: AuditableEntity
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Percent { get; set; }
    }
}
