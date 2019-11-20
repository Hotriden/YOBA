using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOBA_LibraryData.BLL.Entities.Staff
{
    public class Position
    {
        [Required]
        public int PositionId { get; set; }
        [Required]
        public string PositionName { get; set; }
    }
}
