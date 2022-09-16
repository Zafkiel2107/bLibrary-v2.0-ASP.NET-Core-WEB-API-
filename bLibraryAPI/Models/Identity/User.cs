using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace bLibraryAPI.Models.Identity
{
    public class User : IdentityUser
    {
        [Required]
        public int Age { get; set; }
        [Required]
        public virtual IEnumerable<Review> Reviews { get; set; }
        public User()
        {
            Reviews = new List<Review>();
        }
    }
}
