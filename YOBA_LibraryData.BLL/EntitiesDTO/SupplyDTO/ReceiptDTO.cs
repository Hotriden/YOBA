using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YOBA_LibraryData.BLL.Entities.Supply
{
    public class ReceiptDTO
    {
        public int Id { get; set; }
        [Required]
        public string ReceiptName { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public decimal? Price { get; set; }
        [Required]
        public string DocumentNumber { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ReceiptValue { get; set; }
        public bool Shipped { get; set; }
        public bool Paid { get; set; }
    }
}