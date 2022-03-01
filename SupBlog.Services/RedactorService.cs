using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Data.Repositories;
using SupBlog.Data.Repositories.Base;
using SupBlog.Domain;
using SupBlog.Mappers;

namespace SupBlog.Services
{
    public class RedactorService
    {
        private readonly IRepository<Article> _ArticleRepository;
        private readonly CategoryRepository _CategoryRepository;
        private readonly ApplicationDbContext _DbContext;
        private readonly TagRepository _TagRepository;

        public RedactorService(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            _ArticleRepository = new ArticleRepository(dbContext);
            _CategoryRepository = new CategoryRepository(dbContext);
            _TagRepository = new TagRepository(dbContext);
        }

        public async Task<Article> AddArticle(Article article)
        {
            return await _ArticleRepository.Add(article);
        }

        public async Task<IEnumerable<Article>> GetUserArticles(string userId)
        {
            return (await _ArticleRepository.GetAll().ConfigureAwait(false))
                .Where(a => a.User.Id == Guid.Parse(userId));
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _CategoryRepository.GetAll().ConfigureAwait(false);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _DbContext.Categories.FirstOrDefaultAsync(x => x.Name == name).ConfigureAwait(false);
        }

        public List<Tag> GetTagsByNames(params string[] names)
        {
            return names.Select(name => _DbContext.Tags.FirstOrDefault(x => x.Name == name)).ToList();
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            return await _TagRepository.GetAll().ConfigureAwait(false);
        }

        public async Task<ArticleDomain> GetArticleById(int id)
        {
            return (await _ArticleRepository.GetById(id).ConfigureAwait(false)).ToDomain();
        }
    }
}