using SupBlog.Data.Models;

namespace SupBlog.Data.Repositories
{
    public class TagRepository : DbRepository<Tag>
    {
        private readonly ApplicationDbContext _Db;

        public TagRepository(ApplicationDbContext db) : base(db)
        {
            _Db = db;
        }
    }
}