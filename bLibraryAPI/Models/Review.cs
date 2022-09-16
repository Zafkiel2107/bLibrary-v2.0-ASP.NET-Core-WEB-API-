using bLibraryAPI.Models.BaseModel;
using bLibraryAPI.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace bLibraryAPI.Models
{
    public class Review : BaseEntity
    {
        [Required, Display(Name = "Статус")]
        public Status Status { get; set; }
        [Required, Display(Name = "Отзыв")]
        public string UserReview { get; set; }
        [Required, Display(Name = "Рекомендация")]
        public bool IsRecommended { get; set; }
        [DataType(DataType.Date), Display(Name = "Дата написания")]
        public DateTime CreateDate { get; set; }
        [Required]
        public virtual Book Book { get; set; }
        [Required]
        public virtual User User { get; set; }
        public Review()
        {
            this.CreateDate = DateTime.Now;
        }
    }
    public enum Status : byte
    {
        Буду_читать,
        Читаю,
        Прочитана,
        Не_дочитана
    }
}
