using bLibraryAPI.Models.BaseModel;
using System.ComponentModel.DataAnnotations;

namespace bLibraryAPI.Models
{
    public class Genre : BaseEntity
    {
        [Required, Display(Name = "Жанр")]
        public string Name { get; set; }
        [Required]
        public virtual IEnumerable<Book> Books { get; set; }
        public Genre()
        {
            Books = new List<Book>();
        }
    }
}
