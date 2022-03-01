using System.Collections.Generic;
using SupBlog.Data.Models.Base;

namespace SupBlog.Data.Models
{
    public class Tag : Entity
    {
        public virtual string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}