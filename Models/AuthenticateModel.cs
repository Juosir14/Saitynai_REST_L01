using System.ComponentModel.DataAnnotations;

namespace Saitynai_REST_L01.Models
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}