using System.Collections.Generic;
using System.Threading.Tasks;
using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Data.Repositories;
using SupBlog.Data.Repositories.Base;

namespace SupBlog.Services
{
    public class ArticleService
    {
        private readonly ApplicationDbContext _DbContext;
        public readonly IRepository<Article> _ArticleRepository;

        public ArticleService(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            _ArticleRepository = new ArticleRepository(dbContext);
        }
    }
}