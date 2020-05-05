using System.Text.RegularExpressions;

namespace YOBA_Identity.Models
{
    public static class Verification
    {
        static string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

        public static bool VerifyEmail(string email)
        {
            if(Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static string VerificationMessage(string userName, string link)
        {
            string url = "https://yoba.netlify.app/";
            return $"Hello {userName}! There is jus final step to validate your account on <a href=\"{url}\">Your Own Business Application</a>. Click on this " +
                $"verify <a href=\"{link}\">link</a> for finish your registration";
        }
    }
}
