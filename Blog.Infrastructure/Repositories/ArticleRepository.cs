namespace Blog.Infrastructure.Repositories
{
    using Blog.Domain.Entities;
    using Blog.Infrastructure.Persistence;

    public class ArticleRepository : BaseRepository<Article>
    {
        public ArticleRepository(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}