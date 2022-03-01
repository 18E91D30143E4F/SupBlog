using System.Collections.Generic;
using System.Linq;
using SupBlog.Data.Models;
using SupBlog.Domain;

namespace SupBlog.Mappers
{
    public static class ArticleMapper
    {
        public static ArticleDomain ToDomain(this Article article)
        {
            return new()
            {
                Name = article.Name,
                User = article.User,
                Tags = article.Tags,
                Category = article.Category.ToDomain(),
                ShortDescription = article.ShortDescription,
                Description = article.Description,
                HeroImageSource = article.HeroImageSource,
                Id = article.Id
            };
        }

        public static IEnumerable<ArticleDomain> ToDomain(this IEnumerable<Article> articles)
        {
            return articles.Select(article => new ArticleDomain
            {
                Name = article.Name,
                User = article.User,
                Tags = article.Tags,
                Category = article.Category.ToDomain(),
                ShortDescription = article.ShortDescription,
                Description = article.Description,
                HeroImageSource = article.HeroImageSource,
                Id = article.Id
            });
        }

        public static Article ToEntity(this ArticleDomain article)
        {
            return new()
            {
                Name = article.Name,
                Tags = article.Tags,
                ShortDescription = article.ShortDescription,
                Description = article.Description,
                HeroImageSource = article.HeroImageSource,
                Id = article.Id,
                CategoryId = article.Category.Id,
                UserId = article.User.Id
            };
        }
    }
}