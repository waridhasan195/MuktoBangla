using Microsoft.EntityFrameworkCore;
using MuktoBangla.Data;
using MuktoBangla.Model.Domain;

namespace MuktoBangla.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly MuktoBanglaDbContext muktoBanglaDbContext;

        public TagRepository(MuktoBanglaDbContext muktoBanglaDbContext)
        {
            this.muktoBanglaDbContext = muktoBanglaDbContext;
        }

        public async Task<Tag> AddTagAsync(Tag tag)
        {
            await muktoBanglaDbContext.Tags.AddAsync(tag);
            await muktoBanglaDbContext.SaveChangesAsync();
            return tag;
        }

        public Task<Tag?> DeleteTagAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await muktoBanglaDbContext.Tags.ToListAsync();

        }

        public async Task<Tag?> GetSingelTagAsync(Guid Id)
        {
            return await muktoBanglaDbContext.Tags.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Tag?> UpdateTagAsync(Tag tag)
        {
            var existingTag = await muktoBanglaDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.TagDescription = tag.TagDescription;
                await muktoBanglaDbContext.SaveChangesAsync();
                return existingTag;
            }
            else
            {
                return null;
            }
        }
    }
}
