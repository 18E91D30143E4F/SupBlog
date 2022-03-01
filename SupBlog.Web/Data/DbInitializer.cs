using System.Linq;
using Microsoft.EntityFrameworkCore;
using SupBlog.Data;
using SupBlog.Data.Models;

namespace SupBlog.Web.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _Db;

        public DbInitializer(ApplicationDbContext db)
        {
            _Db = db;
        }

        public void Initialize()
        {
            _Db.Database.Migrate();

            if (_Db.Categories.Any() && _Db.Tags.Any()) return;

            foreach (var categoryName in new[] { "Кухня", "Дом", "Столовая", "Компьютеры", "Техника" })
            {
                var category = new Category
                {
                    Name = categoryName
                };
                _Db.Categories.Add(category);
            }

            foreach (var tagName in new[] { "Жизнь-Сказка", "Политологи", "НетОжирению", "ПочемуМы" })
            {
                var tag = new Tag
                {
                    Name = tagName
                };
                _Db.Tags.Add(tag);
            }

            _Db.SaveChanges();
        }
    }
}