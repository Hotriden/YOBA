﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YOBA_LibraryData.BLL.Entities.User
{
    public class Client
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Email { get; set; }
        public string CompanyName { get; set; }
    }
}
