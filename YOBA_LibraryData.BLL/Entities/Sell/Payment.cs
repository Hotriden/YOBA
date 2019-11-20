using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YOBA_LibraryData.BLL.Entities.Sell
{
    public class Payment
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
        public Order Order { get; set; }
        public Customer Cusmoter { get; set; }
        public DateTime PayTime { get; set; }
        public int IdentialPayNumber { get; set; }
    }
}
