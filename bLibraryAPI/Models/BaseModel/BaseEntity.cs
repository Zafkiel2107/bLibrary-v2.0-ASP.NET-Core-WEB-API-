using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace bLibraryAPI.Models.BaseModel
{
    public abstract class BaseEntity
    {
        [Key, Required, HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
