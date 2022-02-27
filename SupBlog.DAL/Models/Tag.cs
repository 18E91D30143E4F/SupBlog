using SupBlog.Data.Models.Base;
using System.Collections.Generic;

namespace SupBlog.Data.Models
{
    public class Tag : Entity
    {
        public virtual string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}