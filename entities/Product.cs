using System.ComponentModel.DataAnnotations;

namespace web_authentication.entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Minimum 4 letter")]
        [MaxLength(20, ErrorMessage = " Maximum 20 leter")]
        public string Name { get; set; }
    }
}
