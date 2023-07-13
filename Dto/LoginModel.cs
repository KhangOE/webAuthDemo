using System.ComponentModel.DataAnnotations;

namespace web_authentication.Dto
{
    public class LoginModel
    {
        [Required]
        [MinLength(4, ErrorMessage = "Minimum 4 letter")]
        [MaxLength(20, ErrorMessage = " Maximum 20 leter")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Minimum 6 letter")]
        [MaxLength(16, ErrorMessage = " Maximum 16 leter")]
        public string Password { get; set; }
    }
}
