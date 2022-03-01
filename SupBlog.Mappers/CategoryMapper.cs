using SupBlog.Data.Models;
using SupBlog.Domain;

namespace SupBlog.Mappers
{
    public static class CategoryMapper
    {
        public static Category ToEntity(this CategoryDomain category)
        {
            return new()
            {
                Articles = category.Articles,
                Id = category.Id,
                Name = category.Name
            };
        }

        public static CategoryDomain ToDomain(this Category category)
        {
            return new()
            {
                Articles = category.Articles,
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}