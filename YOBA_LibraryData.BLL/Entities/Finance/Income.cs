using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Finance
{
    public class Income: AuditableEntity
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
    }
}
