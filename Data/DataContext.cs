//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web_authentication.entities;

namespace web_authentication.Data
{
    public class DataContext : IdentityDbContext<AplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
