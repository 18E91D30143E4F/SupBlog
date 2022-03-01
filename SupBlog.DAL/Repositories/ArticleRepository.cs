using SupBlog.Data.Models;

namespace SupBlog.Data.Repositories
{
    public class ArticleRepository : DbRepository<Article>
    {
        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}