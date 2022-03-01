using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Data.Repositories;
using SupBlog.Data.Repositories.Base;

namespace SupBlog.Services
{
    public class CategoryService
    {
        private readonly IRepository<Category> _CategoryRepository;
        private readonly ApplicationDbContext _DbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            _CategoryRepository = new CategoryRepository(dbContext);
        }
    }
}