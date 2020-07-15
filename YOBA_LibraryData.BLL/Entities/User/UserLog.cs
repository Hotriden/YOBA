using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOBA_LibraryData.DAL.Entities
{
    public class UserLog
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Message { get; set; }

        public string ObjectMessage { get; set; }

        public DateTime Time { get; set; }
    }
}
