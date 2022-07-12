namespace Blog.Infrastructure.Repositories
{
    using Blog.Domain.Entities;
    using Blog.Infrastructure.Persistence;

    using Microsoft.EntityFrameworkCore;

    public class BaseRepository<T> where T : BaseEntity
    {
        public BaseRepository(BlogContext blogContext)
        {
            this.BlogContext = blogContext;
        }

        public BlogContext BlogContext { get; private set; }

        public async Task<T> Create(T itemToCreate)
        {
            await this.BlogContext.Set<T>().AddAsync(itemToCreate);
            await this.BlogContext.SaveChangesAsync();

            return itemToCreate;
        }

        public async Task Delete(params object[] keyValues)
        {
            T item = await this.BlogContext.Set<T>().FindAsync(keyValues);
            if (item != null)
            {
                this.BlogContext.Set<T>().Remove(item);
                await this.BlogContext.SaveChangesAsync();
            }
        }

        public async Task<T> Get(params object[] keyValues)
        {
            return await this.BlogContext.Set<T>().FindAsync(keyValues);
        }

        public async Task<List<T>> List()
        {
            return await this.BlogContext.Set<T>().ToListAsync();
        }

        public async Task<T> Update(T itemToUpdate, params object[] keyValues)
        {
            T item = await this.BlogContext.Set<T>().FindAsync(keyValues);
            this.BlogContext.Entry(item).CurrentValues.SetValues(itemToUpdate);
            await this.BlogContext.SaveChangesAsync();

            return itemToUpdate;
        }
    }
}
