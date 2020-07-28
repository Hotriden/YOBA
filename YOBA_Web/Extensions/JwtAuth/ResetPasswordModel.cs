using System.ComponentModel.DataAnnotations;

namespace YOBA_Web.Models.JwtAuth
{
    /// <summary>
    /// Same as recover model
    /// this one for change password
    /// by email
    /// </summary>
    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
