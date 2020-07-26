using System;
using System.ComponentModel.DataAnnotations;

namespace YOBA_LibraryData.DAL.Entities
{
    public class AuditableEntity
    {
        [Required]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}