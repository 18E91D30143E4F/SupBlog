using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupBlog.Data;
using SupBlog.Data.Models;
using SupBlog.Data.Repositories;
using SupBlog.Data.Repositories.Base;

namespace SupBlog.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly IRepository<Category> _CategoryRepository;

        public CategoryService(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            _CategoryRepository = new CategoryRepository(dbContext);
        }
    }
}