namespace Blog.Infrastructure.Persistence
{
    using Blog.Domain.Entities;

    using Microsoft.EntityFrameworkCore;

    public class BlogContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
    }
}