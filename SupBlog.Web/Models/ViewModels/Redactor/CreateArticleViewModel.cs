using System.Collections.Generic;
using SupBlog.Data.Models;
using SupBlog.Domain;

namespace SupBlog.Web.Models.ViewModels.Redactor
{
    public class CreateArticleViewModel
    {
        public ArticleDomain Article { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<string> AcceptedTags { get; set; }
        public IEnumerable<Tag> AllTags { get; set; }
    }
}