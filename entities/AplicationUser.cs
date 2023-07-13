//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
namespace web_authentication.entities
{
    public class AplicationUser : IdentityUser
    {

        public string Name { get; set; } = "name";
        public string Email { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now.AddYears(-10);

        //public int gender { get; set; }

    }
}
