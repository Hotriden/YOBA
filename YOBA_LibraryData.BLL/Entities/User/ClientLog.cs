using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YOBA_LibraryData.BLL.Entities.User;

namespace YOBA_LibraryData.DAL.Entities.User
{
    public class ClientLog: AuditableEntity
    {
        [Required]
        public Client Client { get; set; }
        public string Message { get; set; }
    }
}
