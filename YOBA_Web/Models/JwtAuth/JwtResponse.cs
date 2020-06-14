using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YOBA_Web.Models.JwtAuth
{
    public class JwtReponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
