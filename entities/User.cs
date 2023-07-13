using Microsoft.AspNetCore.Identity;

namespace web_authentication.entities
{
    public class User 
    {
        public int Id { get;set; }

        public string Name { get;set; }

        public string email { get;set; }
    }
}
