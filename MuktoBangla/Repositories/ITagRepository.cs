using MuktoBangla.Model.Domain;

namespace MuktoBangla.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag?> GetSingelTagAsync(Guid Id);
        Task<Tag> AddTagAsync(Tag tag);
        Task<Tag?> UpdateTagAsync(Tag tag);
        Task<Tag?> DeleteTagAsync(Guid Id);

    }
}
