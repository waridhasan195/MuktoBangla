using Microsoft.EntityFrameworkCore;
using MuktoBangla.Data;
using MuktoBangla.Model.Domain;

namespace MuktoBangla.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly MuktoBanglaDbContext muktoBanglaDbContext;

        public BlogPostRepository(MuktoBanglaDbContext muktoBanglaDbContext)
        {
            this.muktoBanglaDbContext = muktoBanglaDbContext;
        }

        public async Task<BlogPost> AddBlogPostAsync(BlogPost blogPost)
        {
            await muktoBanglaDbContext.BlogPosts.AddAsync(blogPost);
            await muktoBanglaDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteBlogPostAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostAssync()
        {
            return await muktoBanglaDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync(); 
        }

        public async Task<BlogPost?> GetSingelBlogPostAsync(Guid Id)
        {
            return await muktoBanglaDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.ID == Id);
        }

        public Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
