using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using YOBA_LibraryData.DAL.Entities;

namespace YOBA_LibraryData.BLL.Entities.Finance
{
    public class Tax: AuditableEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Percent { get; set; }
    }
}
