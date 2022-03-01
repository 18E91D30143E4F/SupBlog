using System.Collections.Generic;
using System.Threading.Tasks;
using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Data.Repositories;
using SupBlog.Data.Repositories.Base;

namespace SupBlog.Services
{
    public class GuestService
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly IRepository<Article> _ArticleRepository;

        public GuestService(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            _ArticleRepository = new ArticleRepository(dbContext);
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await _ArticleRepository.GetAll().ConfigureAwait(false);
        }

        public async Task<Article> GetArticleById(int id)
        {
            return await _ArticleRepository.GetById(id).ConfigureAwait(false);
        }
    }
}