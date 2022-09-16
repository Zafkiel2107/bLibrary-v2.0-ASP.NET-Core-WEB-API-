using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace bLibraryAPI.Models.Identity
{
    public class Role : IdentityRole
    {
        [Required]
        public string Description { get; set; }
    }
}
