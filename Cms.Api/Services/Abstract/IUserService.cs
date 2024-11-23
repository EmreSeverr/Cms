using Cms.Api.DTO;
using Cms.Api.Filters.Concrate;

namespace Cms.Api.Services.Abstract
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserDto user);
        Task<IEnumerable<UserDto>> ListUsersAsync(UserFilter userFilter);

        Task<UserDto> GetContentsByUserAsync(int userId);
        Task AddContentAsync(AddContentDto addContentDto);
        Task AddVariantToContentAsync(AddContentVariantDto addContentVariantDto);
        Task DeleteContentAsync(int contentId);
    }
}
