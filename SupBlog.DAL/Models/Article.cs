using SupBlog.Data.Models.Base;
using System.Collections.Generic;

namespace SupBlog.Data.Models
{
    public class Article : Entity
    {
        public virtual string Name { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string Description { get; set; }
        public virtual string HeroImageSource { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}