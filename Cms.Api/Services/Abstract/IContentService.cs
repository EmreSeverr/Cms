using Cms.Api.DTO;
using Cms.Api.Filters.Concrate;

namespace Cms.Api.Services.Abstract
{
    public interface IContentService
    {
        Task<ContentListDto> GetContentByIdAsync(int signedUserId, int contentId, int languageId);
        Task<IEnumerable<ContentListDto>> GetContentsByUserAsync(int signedUserId, ContentFilter contentFilter);
    }
}
