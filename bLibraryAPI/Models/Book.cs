using bLibraryAPI.Models.BaseModel;
using System.ComponentModel.DataAnnotations;

namespace bLibraryAPI.Models
{
    public class Book : BaseEntity
    {
        [Required, Display(Name = "Название книги")]
        public string Name { get; set; }
        [Required, Display(Name = "Автор")]
        public string Author { get; set; }
        [Required, Display(Name = "Количество рекомендаций")]
        public int RecommendationsNum { get; set; }
        [Required, Display(Name = "Жанр")]
        public virtual Genre Genre { get; set; }
        [Required, Display(Name = "Часть")]
        public int Part { get; set; }
        [Required, Display(Name = "Страниц")]
        public int Pages { get; set; }
        [Required, Display(Name = "Язык")]
        public Language Language { get; set; }
        [Required, Display(Name = "Описание")]
        public string Description { get; set; }
        [Required, Display(Name = "Ссылка на обложку")]
        public string CoverLink { get; set; }
        [Required, Display(Name = "Ссылка на книгу")]
        public string BookLink { get; set; }
        [Required, Display(Name = "Отзывы")]
        public virtual IEnumerable<Review> Reviews { get; set; }
        public Book()
        {
            RecommendationsNum = 0;
            Reviews = new List<Review>();
        }
    }
    public enum Language : byte
    {
        Русский,
        Английский
    }
}
