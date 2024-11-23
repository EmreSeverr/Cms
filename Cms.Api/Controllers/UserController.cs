using Cms.Api.Cache.Abstract;
using Cms.Api.Cache.Concrate;
using Cms.Api.DTO;
using Cms.Api.Filters.Concrate;
using Cms.Api.Services.Abstract;
using Cms.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;

        public UserController(IUserService userService, ICacheService cacheService)
        {
            _userService = userService;
            _cacheService = cacheService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(AddUserDto userDto)
        {
            return await _userService.AddUserAsync(userDto).ConfigureAwait(false).GetApiResponseAsync(HttpContext, "Kullanici basariyla eklenmistir.");
        }

        [HttpPut]
        public async Task<IActionResult> GetUsersAsync(UserFilter userFilter)
        {
            var users = await _cacheService.ConfigureSetGetAsync(CacheService.GetCacheKey(HttpContext, userFilter, prefix: CacheService.UserPrefix), async () =>
            {
                return await _userService.ListUsersAsync(userFilter).ConfigureAwait(false);
            });

            return users.GetApiResponse(HttpContext);
        }

        [HttpGet("contents/{userId}")]
        public async Task<IActionResult> GetContentsByUserAsync(int userId)
        {
            var users = await _cacheService.ConfigureSetGetAsync(CacheService.GetCacheKey(HttpContext, prefix: $"{CacheService.ContentPrefix}-{userId}"), async () =>
            {
                return await _userService.GetContentsByUserAsync(userId).ConfigureAwait(false);
            });

            return users.GetApiResponse(HttpContext);
        }

        [HttpPost("content")]
        public async Task<IActionResult> AddContentAsync(AddContentDto addContentDto)
        {
            return await _userService.AddContentAsync(addContentDto).ConfigureAwait(false).GetApiResponseAsync(HttpContext, "Icerik basariyla eklenmistir.");
        }

        [HttpPost("content/variant")]
        public async Task<IActionResult> AddVariantToContentAsync(AddContentVariantDto addContentVariantDto)
        {
            return await _userService.AddVariantToContentAsync(addContentVariantDto).ConfigureAwait(false).GetApiResponseAsync(HttpContext, "Icerik varyantlari basariyla eklenmistir.");
        }
    }
}
