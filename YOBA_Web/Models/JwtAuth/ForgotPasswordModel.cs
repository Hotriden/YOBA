using System.ComponentModel.DataAnnotations;

namespace YOBA_Web.Models.JwtAuth
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
