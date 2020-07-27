using System.ComponentModel.DataAnnotations;

namespace YOBA_Web.Models.JwtAuth
{
    /// <summary>
    /// Simple user model for
    /// recover password by email
    /// </summary>
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
