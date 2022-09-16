using bLibraryAPI.Models;
using bLibraryAPI.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bLibraryAPI.ConnectionManager
{
    internal class bLibraryDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public bLibraryDbContext(DbContextOptions<bLibraryDbContext> options)
            : base(options)
        {
        }
    }
}
