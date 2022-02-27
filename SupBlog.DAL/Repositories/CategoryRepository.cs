using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupBlog.Data.Models;

namespace SupBlog.Data.Repositories
{
    public class CategoryRepository : DbRepository<Category>
    {
        private readonly ApplicationDbContext _Db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _Db = db;
        }
    }
}