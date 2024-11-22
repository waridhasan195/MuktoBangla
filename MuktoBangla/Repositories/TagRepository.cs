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

        public Task<Tag> DeleteTagAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> EditTagAsync(Tag tag)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetSingelTagAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> UpdateTagAsync(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
