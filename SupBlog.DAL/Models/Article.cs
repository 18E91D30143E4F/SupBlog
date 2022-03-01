using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupBlog.Data.Models.Base;

namespace SupBlog.Data.Models
{
    public class Article : Entity
    {
        [Required]
        public virtual string Name { get; set; }
        public virtual string ShortDescription { get; set; }
        [Required]
        public virtual string Description { get; set; }
        public virtual string HeroImageSource { get; set; }

        [ForeignKey("Category")]
        public virtual int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        [ForeignKey("User")] public virtual Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}