﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOBA_Web.Models
{
    public static class Constants
    {
        public const string Issuer = Audiance;
        public const string Audiance = "https://localhost:44382/";
        public const string Secret = "not_too_short_secret_otherwise_it_might_error";
    }
}
