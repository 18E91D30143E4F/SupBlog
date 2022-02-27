using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupBlog.Data.Models;
using System;

namespace SupBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Article>()
                .HasOne(p => p.User)
                .WithMany(t => t.Articles);

            builder.Entity<Article>()
                .HasOne(p => p.Category)
                .WithMany(t => t.Articles);

            builder.Entity<Article>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Articles);
        }
    }
}
