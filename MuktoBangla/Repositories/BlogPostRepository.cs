using Microsoft.EntityFrameworkCore;
using MuktoBangla.Data;
using MuktoBangla.Model.Domain;
using Syncfusion.EJ2.PdfViewer;

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

        public async Task<BlogPost?> DeleteBlogPostAsync(Guid Id)
        {
            var deletedItem = await muktoBanglaDbContext.BlogPosts.FindAsync(Id);
            if (deletedItem != null)
            {
                muktoBanglaDbContext.BlogPosts.Remove(deletedItem);
                await muktoBanglaDbContext.SaveChangesAsync();
                return deletedItem;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostAssync()
        {
            return await muktoBanglaDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync(); 
        }

        public async Task<BlogPost?> GetSingelBlogPostAsync(Guid Id)
        {
            return await muktoBanglaDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x=>x.ID == Id);
        }

        public async Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost)
        {
            var existingBlog = await muktoBanglaDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.ID==blogPost.ID);
            if(existingBlog != null)
            {
                //existingBlog.ID = blogPost.ID;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Description = blogPost.Description;
                existingBlog.Author = blogPost.Author;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;
                await muktoBanglaDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
