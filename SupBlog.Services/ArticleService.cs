using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Data.Repositories;
using SupBlog.Data.Repositories.Base;

namespace SupBlog.Services
{
    public class ArticleService
    {
        public readonly IRepository<Article> _ArticleRepository;
        private readonly ApplicationDbContext _DbContext;

        public ArticleService(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            _ArticleRepository = new ArticleRepository(dbContext);
        }
    }
}