//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace web_authentication.entities
{
    public class AplicationUser : IdentityUser
    {

        [Required]
        [MinLength(4,ErrorMessage = "Minimum 4 letter")]
        [MaxLength(20,ErrorMessage = " Maximum 20 leter")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now.AddYears(-10);

        //public int gender { get; set; }

    }
}
