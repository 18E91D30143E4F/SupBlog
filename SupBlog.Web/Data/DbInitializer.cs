using Microsoft.EntityFrameworkCore;
using SupBlog.Data;
using SupBlog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupBlog.Web.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _Db;

        public DbInitializer(ApplicationDbContext db) => _Db = db;

        public void Initialize()
        {
            _Db.Database.Migrate();

            if (_Db.Articles.Any()) return;

            for (var i = 0; i < 5; i++)
            {
                var article = new Article
                {
                    Category = new Category
                    {
                        Name = $"Category {i}"
                    },
                    Description = $"Description {i}",
                    Name = $"Article {i}",
                    HeroImageSource = $"Image src {i}",
                    ShortDescription = $"Short {i}"
                };

                var tags = new List<Tag>();

                for (var (j, count) = (0, new Random().Next(5, 10)); j < count; j++)
                {
                    tags.Add(new Tag
                    {
                        Name = $"Tag {j}"
                    });
                }

                article.Tags = tags;
                _Db.Articles.Add(article);
            }

            _Db.SaveChanges();
        }
    }
}