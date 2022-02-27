using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SupBlog.Data.Models;

namespace SupBlog.Domain
{
    public class CategoryDomain
    {
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public virtual string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}