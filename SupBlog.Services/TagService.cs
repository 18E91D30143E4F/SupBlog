using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Data.Repositories;
using SupBlog.Data.Repositories.Base;

namespace SupBlog.Services
{
    public class TagService
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly IRepository<Tag> _TagRepository;

        public TagService(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            _TagRepository = new TagRepository(dbContext);
        }
    }
}