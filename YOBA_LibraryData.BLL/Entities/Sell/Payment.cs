using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Sell
{
    public class Payment: AuditableEntity
    {
        public string Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        public Order Order { get; set; }
        public Customer Cusmoter { get; set; }
        public DateTime PayTime { get; set; }
        [Required]
        public string IdentialPayNumber { get; set; }
    }
}
