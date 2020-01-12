using System;
using System.Collections.Generic;
using System.Text;

namespace YOBA_LibraryData.DAL.Entities
{
    public class UserLog
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string ObjectMessage { get; set; }

        public DateTime Time { get; set; }
    }
}
