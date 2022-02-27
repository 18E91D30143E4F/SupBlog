using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SupBlog.Data.Models;

namespace SupBlog.Domain
{
    public class ArticleDomain
    {
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "Название статьи")]

        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Краткое описание")]
        public virtual string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public virtual string Description { get; set; }

        [Required]
        [Display(Name = "Путь к картинке")]
        public virtual string HeroImageSource { get; set; }

        public virtual CategoryDomain Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}