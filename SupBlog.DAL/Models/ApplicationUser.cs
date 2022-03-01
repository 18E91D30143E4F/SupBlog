using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SupBlog.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Article> Articles { get; set; }
    }
}