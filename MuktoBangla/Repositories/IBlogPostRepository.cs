using MuktoBangla.Model.Domain;

namespace MuktoBangla.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllBlogPostAssync();
        Task<BlogPost?> GetSingelBlogPostAsync(Guid Id);
        Task<BlogPost> AddBlogPostAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteBlogPostAsync(Guid Id);
    }
}
 